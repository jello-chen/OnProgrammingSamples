using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterpreterPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpressionParser parser = new ExpressionParser();
            Expression expression = parser.Parse("-a1 + (-0.1 + 0.2 * (a1 * + a2) + 0.1 + (3 - (a2)) * 0.3) * (66 / 22 + 3)");
            ExpressionContext context = new ExpressionContext(new Dictionary<string, object>
            {
                ["a1"] = 1,
                ["a2"] = 1,
            });
            double result = (double)expression.Evaluate(context);
            Console.WriteLine(result);

            Console.Read();
        }
    }

    class ExpressionContext
    {
        private readonly Dictionary<string, object> scope;

        public ExpressionContext() => this.scope = new Dictionary<string, object>();

        public ExpressionContext(Dictionary<string, object> scope) => this.scope = scope;

        public bool ContainsKey(string key) => this.scope.ContainsKey(key);

        public object Get(string key) => this.scope[key];

        public void Add(string key, object value) => this.scope.Add(key, value);

        public void Remove(string key) => this.scope.Remove(key);

        public void Clear() => this.scope.Clear();
    }

    abstract class Expression
    {
        public abstract ExpressionType ExpressionType { get; }
        public abstract object Evaluate(ExpressionContext context);
    }

    class Literal : Expression
    {
        private readonly object value;
        public Literal(object value) => this.value = value;

        public override ExpressionType ExpressionType => ExpressionType.Literal;

        public override object Evaluate(ExpressionContext context) => this.value;
    }

    class Variable : Expression
    {
        private readonly string variableName;
        public Variable(string variableName) => this.variableName = variableName;

        public override ExpressionType ExpressionType => ExpressionType.Variable;

        public override object Evaluate(ExpressionContext context) => context.Get(variableName);
    }

    abstract class BinaryExpression : Expression
    {
        private readonly Expression left, right;
        public BinaryExpression(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        protected Expression Left => this.left;
        protected Expression Right => this.right;
    }

    class AddExpression : BinaryExpression
    {
        public AddExpression(Expression left, Expression right) : base(left, right)
        {

        }

        public override ExpressionType ExpressionType => ExpressionType.Add;

        public override object Evaluate(ExpressionContext context)
        {
            return Convert.ToDouble(Left.Evaluate(context)) + Convert.ToDouble(Right.Evaluate(context));
        }
    }

    class SubExpression : BinaryExpression
    {
        public SubExpression(Expression left, Expression right) : base(left, right)
        {

        }

        public override ExpressionType ExpressionType => ExpressionType.Sub;

        public override object Evaluate(ExpressionContext context)
        {
            return Convert.ToDouble(Left.Evaluate(context)) - Convert.ToDouble(Right.Evaluate(context));
        }
    }

    class MulExpression : BinaryExpression
    {
        public MulExpression(Expression left, Expression right) : base(left, right)
        {

        }

        public override ExpressionType ExpressionType => ExpressionType.Mul;

        public override object Evaluate(ExpressionContext context)
        {
            return Convert.ToDouble(Left.Evaluate(context)) * Convert.ToDouble(Right.Evaluate(context));
        }
    }

    class DivExpression : BinaryExpression
    {
        public DivExpression(Expression left, Expression right) : base(left, right)
        {

        }

        public override ExpressionType ExpressionType => ExpressionType.Div;

        public override object Evaluate(ExpressionContext context)
        {
            return Convert.ToDouble(Left.Evaluate(context)) / Convert.ToDouble(Right.Evaluate(context));
        }
    }

    class ExpressionParser
    {

        private readonly Stack<Expression> operandStack = new Stack<Expression>();
        private readonly Stack<string> operatorStack = new Stack<string>();

        public Expression Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            ExpressionTokenizer tokenizer = new ExpressionTokenizer(input);
            while (tokenizer.HasToken())
            {
                Token token = tokenizer.NextToken();
                TokenType tokenType = token.Type;
                string tokenValue = token.Value;
                if (tokenType == TokenType.Operator)
                {
                    switch (tokenValue)
                    {
                        case "(":
                            if (operatorStack.Contains(")"))
                                throw new Exception("Illigal expression");
                            operatorStack.Push(tokenValue);
                            break;
                        case ")":
                            if (!operatorStack.Contains("("))
                                throw new Exception("Illigal expression");
                            while (operatorStack.Count > 0)
                            {
                                string op = operatorStack.Pop();
                                if (op == "(") break;
                                Expression right = operandStack.Pop();
                                Expression left = operandStack.Pop();
                                operandStack.Push(ParseBinaryExpression(op, left, right));
                            }
                            break;
                        case "+":
                        case "-":
                            if (operatorStack.Count == 0)
                            {
                                operatorStack.Push(tokenValue);
                            }
                            else
                            {
                                if (operatorStack.Peek() != "(")
                                {
                                    string op = operatorStack.Pop();
                                    Expression right = operandStack.Pop();
                                    Expression left = operandStack.Pop();
                                    operandStack.Push(ParseBinaryExpression(op, left, right));
                                }
                                operatorStack.Push(tokenValue);
                            }
                            break;
                        case "*":
                        case "/":
                            if (operatorStack.Count == 0)
                            {
                                operatorStack.Push(tokenValue);
                            }
                            else
                            {
                                var _op = operatorStack.Peek();
                                if (_op != "+" && _op != "-" && _op != "(")
                                {
                                    string op = operatorStack.Pop();
                                    Expression right = operandStack.Pop();
                                    Expression left = operandStack.Pop();
                                    operandStack.Push(ParseBinaryExpression(op, left, right));
                                }
                                operatorStack.Push(tokenValue);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (tokenType == TokenType.Literal)
                    {
                        operandStack.Push(new Literal(tokenValue));
                    }
                    else
                    {
                        operandStack.Push(new Variable(tokenValue));
                    }
                }
            }

            while (operatorStack.Count > 0)
            {
                string op = operatorStack.Pop();
                Expression right = operandStack.Pop();
                Expression left = operandStack.Pop();
                operandStack.Push(ParseBinaryExpression(op, left, right));
            }

            if (operandStack.Count != 1)
                throw new Exception("Invalid expression");

            return operandStack.Pop();
        }

        private BinaryExpression ParseBinaryExpression(string op, Expression left, Expression right)
        {
            switch (op)
            {
                case "+":
                    return new AddExpression(left, right);
                case "-":
                    return new SubExpression(left, right);
                case "*":
                    return new MulExpression(left, right);
                case "/":
                    return new DivExpression(left, right);
                default:
                    throw new Exception("Illigal operator");
            }
        }
    }

    class ExpressionTokenizer
    {
        private static readonly Regex LiteralRegex = new Regex("^[1-9]*[0-9](?:(\\.[0-9]{1,})?)$", RegexOptions.Compiled);
        private static readonly Regex VariableRegex = new Regex("^[_a-z]{1,}[_a-z0-9]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly string[] Operators = new string[] { "+", "-", "*", "/" };

        private int currentPos;
        private List<Token> tokens;
        private string input;
        private int flag = 0;

        public ExpressionTokenizer(string input)
        {
            this.input = input;
            tokens = new List<Token>();
            currentPos = 0;
            List<char> cToken = new List<char>(64);

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                switch (c)
                {
                    case '(':
                    case ')':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        if (cToken.Count > 0)
                        {
                            if (!CheckToken(cToken))
                                throw new Exception($"Illigal token `{new string(cToken.ToArray())}`");
                            tokens.Add(new Token { Value = new string(cToken.ToArray()), Type = flag == 0 ? TokenType.Literal : TokenType.Variable });
                            cToken.Clear();
                        }
                        HandleNegativeNumberToken(c);
                        HandleConsecutiveOperators(c);
                        tokens.Add(new Token { Value = c.ToString(), Type = TokenType.Operator });
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        if (cToken.Count == 0)
                        {
                            if (c == '.')
                                throw new Exception($"Illigal character `{c}`");
                            flag = 0;
                        }
                        cToken.Add(c);
                        break;
                    default:
                        if (!char.IsWhiteSpace(c))
                        {
                            if (!char.IsLetter(c))
                                throw new Exception($"Illigal character `{c}`");
                            if (cToken.Count == 0)
                            {
                                flag = 1;
                            }
                            cToken.Add(c);
                        }
                        break;
                }
            }

            if (cToken.Count > 0)
            {
                if (!CheckToken(cToken))
                    throw new Exception($"Illigal token `{new string(cToken.ToArray())}`");
                tokens.Add(new Token { Value = new string(cToken.ToArray()), Type = flag == 0 ? TokenType.Literal : TokenType.Variable });
                cToken.Clear();
            }
        }

        public bool HasToken() => currentPos < tokens.Count;

        public Token NextToken() => tokens[currentPos++];

        private bool CheckToken(List<char> cToken)
        {
            if (flag == 0 && LiteralRegex.IsMatch(new string(cToken.ToArray())))
                return true;
            if (flag == 1 && VariableRegex.IsMatch(new string(cToken.ToArray())))
                return true;
            return false;
        }

        private void HandleNegativeNumberToken(char c)
        {
            if (c == '-')
            {
                if (tokens.Count == 0)
                {
                    tokens.Add(new Token { Value = "0", Type = TokenType.Literal });
                }
                else
                {
                    Token token = tokens.Last();
                    if (token.Type == TokenType.Operator)
                    {
                        if (token.Value == "(")
                        {
                            tokens.Add(new Token { Value = "0", Type = TokenType.Literal });
                        }
                    }
                }
            }
        }

        private void HandleConsecutiveOperators(char c)
        {
            if(tokens.Count > 0 && Operators.Contains(c.ToString()))
            {
                Token token = tokens.Last();
                if (token.Type == TokenType.Operator)
                {
                    if (Operators.Contains(token.Value))
                    {
                        tokens.Remove(token);
                    }
                }
            }
        }
    }

    class Token
    {
        public string Value { get; set; }
        public TokenType Type { get; set; }

        public override string ToString()
        {
            return $"{{Type: \"{Type}\", Value: \"{Value}\"}}";
        }
    }

    enum ExpressionType
    {
        Literal,
        Variable,
        Add,
        Sub,
        Mul,
        Div,
    }

    enum TokenType
    {
        Operator,
        Literal,
        Variable,
    }
}

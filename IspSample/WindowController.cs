namespace IspSample
{
    abstract class WindowController
    {
        protected abstract void OnWindowOpen(Window window);
        protected abstract void OnWindowClose(Window window);
        protected abstract void OnWindowMoved(Window window);
    }
}

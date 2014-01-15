using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using log4net;

namespace Chief2moro.ImageDataExtensions
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class ImageResizeDescriptorInitialization : IInitializableModule
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ImageResizeDescriptorInitialization));

        private bool _initialized;
        private readonly ImageResizeContentEvents _imageResizeContentEvents = new ImageResizeContentEvents();

        public void Initialize(InitializationEngine context)
        {
            if (_initialized)
                return;

            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();
            contentEvents.PublishingContent += _imageResizeContentEvents.contentEvents_PublishingContent;
            _logger.Info("Event _imageResizeContentEvents.contentEvents_PublishingContent is handled");
            _initialized = true;
        }

        public void Preload(string[] parameters)
        {
             
        }

        public void Uninitialize(InitializationEngine context)
        {
            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();
            contentEvents.PublishingContent -= _imageResizeContentEvents.contentEvents_PublishingContent;
            _logger.Info("Event _imageResizeContentEvents.contentEvents_PublishingContent is removed");
        }
    }
}
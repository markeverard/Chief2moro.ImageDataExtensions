using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace Chief2moro.ImageDataExtensions
{
    [InitializableModule]
    public class ImageDataExtensionsConfigurationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddHttpContextOrThreadScoped<IResizeBlobProcessor, ThumbnailManagerProcessor>();
        }
        
        public void Initialize(InitializationEngine context)
        {   
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
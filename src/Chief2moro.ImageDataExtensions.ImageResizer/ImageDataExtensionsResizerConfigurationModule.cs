using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace Chief2moro.ImageDataExtensions.ImageResizer
{
    [InitializableModule]
    [ModuleDependency(typeof(ImageDataExtensionsConfigurationModule))]
    public class ImageDataExtensionsResizerConfigurationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.AddHttpContextOrThreadScoped<IResizeBlobProcessor, ImageResizerProcessor>();
        }
        
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }
}
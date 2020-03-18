using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComicReaderApp.Extensions
{
    [ContentProperty(nameof(IconImageSource))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string IconImageSource { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (IconImageSource == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(IconImageSource, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }
    }
}

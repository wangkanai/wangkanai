using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Wangkanai.Detection.UI
{
    internal class ResponsivePageModelConvention : IPageApplicationModelConvention
    {
        public void Apply(PageApplicationModel model)
        {
            var defaultUIAttribute = model.ModelType.GetCustomAttribute<ResponsiveDefaultUIAttribute>();
            if (defaultUIAttribute == null)
                return;

            ValidateTemplate(defaultUIAttribute.Template);
            model.ModelType = defaultUIAttribute.Template.GetTypeInfo();
        }

        private void ValidateTemplate(Type template)
        {
            if (template.IsAbstract || template.IsGenericTypeDefinition)
                throw new InvalidOperationException("Implementation type can't be abstract or generic");
        }
    }
}
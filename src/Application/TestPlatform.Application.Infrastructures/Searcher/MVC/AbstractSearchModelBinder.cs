namespace TestPlatform.Application.Infrastructures.Searcher.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using TestPlatform.Application.Infrastructures.Searcher.Types;

    public class AbstractSearchModelBinder : IModelBinder
    {
        private readonly IDictionary<Type, ComplexTypeModelBinder> modelBuilderByType;

        private readonly IModelMetadataProvider modelMetadataProvider;

        public AbstractSearchModelBinder(IDictionary<Type, ComplexTypeModelBinder> modelBuilderByType, IModelMetadataProvider modelMetadataProvider)
        {
            this.modelBuilderByType = modelBuilderByType ?? throw new ArgumentNullException(nameof(modelBuilderByType));
            this.modelMetadataProvider = modelMetadataProvider ?? throw new ArgumentNullException(nameof(modelMetadataProvider));
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelTypeName = bindingContext.ValueProvider.GetValue(ModelNames.CreatePropertyModelName(bindingContext.ModelName, "ModelTypeName"));

            if (modelTypeName.FirstValue != null)
            {
                Type modelType = this.GetSearchTypeFromTypeName(modelTypeName.FirstValue)!;
                if (this.modelBuilderByType.TryGetValue(modelType, out var modelBinder))
                {
                    ModelBindingContext innerModelBindingContext = DefaultModelBindingContext.CreateBindingContext(
                        bindingContext.ActionContext,
                        bindingContext.ValueProvider,
                        this.modelMetadataProvider.GetMetadataForType(modelType),
                        null,
                        bindingContext.ModelName);

                    modelBinder.BindModelAsync(innerModelBindingContext);

                    bindingContext.Result = innerModelBindingContext.Result;
                    return Task.CompletedTask;
                }
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        private Type GetSearchTypeFromTypeName(string typeName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var searchClasses = assembly.GetExportedTypes()
                .Where(t => typeof(AbstractSearch).Equals(t.BaseType))
                .Where(t => !t.IsAbstract)
                .ToList();

            foreach (var searchClass in searchClasses)
            {
                if (searchClass.Name == typeName)
                {
                    return searchClass;
                }
            }

            return null;
        }
    }
}

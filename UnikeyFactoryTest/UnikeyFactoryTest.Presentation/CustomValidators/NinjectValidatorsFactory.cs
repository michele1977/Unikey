using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Ninject;

namespace UnikeyFactoryTest.Presentation.CustomValidators
{
    public class NinjectValidatorFactory : ValidatorFactoryBase
    {
        private readonly IKernel _kernel;

        public NinjectValidatorFactory()
        {
            _kernel = new StandardKernel();
        }

        public NinjectValidatorFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (!_kernel.GetBindings(validatorType).Any())
            {
                return null;
            }

            return _kernel.Get(validatorType) as IValidator;
        }
    }
}
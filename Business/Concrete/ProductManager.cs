using Business.Abstract;
using Business.Constant;
using Business.Validation.FluentVaidation;
using Core.Aspect.Autofac.Cache;
using Core.Aspect.Autofac.Logging;
using Core.Aspect.Autofac.Preformance;
using Core.Aspect.Autofac.Transaction;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcern.Logging.Log4Net.Logger;
using Core.Utility.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IDataResult<Product> Add(Product product)
        {
          return new SuccessDataResult<Product>(_productDal.Add(product),Message.ProductAdd,Message.ProductAddId);
        }

        [CacheAspect(1)]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccesessResult(Message.ProductDelete,Message.ProductDeleteId);
        }
        [CacheAspect(1)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>( _productDal.Get(f => f.ProductID == productId));
        }
        [PerformanceAspect(1)]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetList().ToList());
        }

        [LogAspect(typeof(JsonFileLogger))]
        public IDataResult<List<Product>> GetListByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(g => g.CategoryId == categoryId).ToList());
        }

        //[TransactionScopeAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccesessResult(Message.ProductUpdate,Message.ProductUpdateId);
        }
    }
}

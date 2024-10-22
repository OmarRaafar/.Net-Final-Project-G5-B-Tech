﻿using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Contracts_B.Product
{
    public interface IProductSpecificationTranslationRepository: IGenericRepositoryB<ProductSpecificationTranslationB>
    {
        Task<IQueryable<ProductSpecificationTranslationB>> GetTranslationsByProductId(int productId,string language);
    }
}
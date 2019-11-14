﻿using OpenActive.DatasetSite.NET;
using OpenActive.NET;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenActive.Server.NET.OpenBookingHelper
{
    public class ModelSupport<TComponents> where TComponents : class, IBookableIdComponents, new()
    {
        private BookablePairIdTemplate<TComponents> IdTemplate { get; set; }
        public SingleIdTemplate<SellerIdComponents> SellerIdTemplate { get; private set; }

        protected internal void SetConfiguration(BookablePairIdTemplate<TComponents> template, SingleIdTemplate<SellerIdComponents> sellerTemplate)
        {
            this.IdTemplate = template;
            this.SellerIdTemplate = sellerTemplate;
        }

        /// <summary>
        /// Use OpportunityType from components
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        protected Uri RenderSellerId(SellerIdComponents components)
        {
            return SellerIdTemplate.RenderId(components);
        }

        /// <summary>
        /// Use OpportunityType from components
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        protected Uri RenderOpportunityId(TComponents components)
        {
            return IdTemplate.RenderOpportunityId(components);
        }
        /// <summary>
        /// Use OpportunityType from components
        /// </summary>
        /// <param name="components"></param>
        /// <returns></returns>
        protected Uri RenderOfferId(TComponents components)
        {
            return IdTemplate.RenderOfferId(components);
        }
        protected Uri RenderOpportunityId(OpportunityType opportunityType, TComponents components)
        {
            return IdTemplate.RenderOpportunityId(opportunityType, components);
        }

        protected Uri RenderOfferId(OpportunityType opportunityType, TComponents components)
        {
            return IdTemplate.RenderOfferId(opportunityType, components);
        }
    }
}

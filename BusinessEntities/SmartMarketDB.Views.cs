//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(BusinessEntities.SmartMarketDB),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySets9f83466be4b61c1c28db55e81e6fd4b89ac387b38b86df1662355c938c4f37b5))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework Power Tools", "0.9.0.0")]
    internal sealed class ViewsForBaseEntitySets9f83466be4b61c1c28db55e81e6fd4b89ac387b38b86df1662355c938c4f37b5 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "9f83466be4b61c1c28db55e81e6fd4b89ac387b38b86df1662355c938c4f37b5"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "CodeFirstDatabase.Category")
            {
                return GetView0();
            }

            if (extentName == "CodeFirstDatabase.Company")
            {
                return GetView1();
            }

            if (extentName == "CodeFirstDatabase.User")
            {
                return GetView2();
            }

            if (extentName == "CodeFirstDatabase.Customer")
            {
                return GetView3();
            }

            if (extentName == "CodeFirstDatabase.OrderItem")
            {
                return GetView4();
            }

            if (extentName == "CodeFirstDatabase.Order")
            {
                return GetView5();
            }

            if (extentName == "CodeFirstDatabase.Product")
            {
                return GetView6();
            }

            if (extentName == "CodeFirstDatabase.SubCategory")
            {
                return GetView7();
            }

            if (extentName == "CodeFirstDatabase.Role")
            {
                return GetView8();
            }

            if (extentName == "CodeFirstDatabase.UserRole")
            {
                return GetView9();
            }

            if (extentName == "SmartMarketDB.Categories")
            {
                return GetView10();
            }

            if (extentName == "SmartMarketDB.Companies")
            {
                return GetView11();
            }

            if (extentName == "SmartMarketDB.Users")
            {
                return GetView12();
            }

            if (extentName == "SmartMarketDB.Customers")
            {
                return GetView13();
            }

            if (extentName == "SmartMarketDB.OrderItems")
            {
                return GetView14();
            }

            if (extentName == "SmartMarketDB.Orders")
            {
                return GetView15();
            }

            if (extentName == "SmartMarketDB.Products")
            {
                return GetView16();
            }

            if (extentName == "SmartMarketDB.SubCategories")
            {
                return GetView17();
            }

            if (extentName == "SmartMarketDB.Roles")
            {
                return GetView18();
            }

            if (extentName == "SmartMarketDB.UserRoles")
            {
                return GetView19();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Category.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Category
        [CodeFirstDatabaseSchema.Category](T1.Category_Id, T1.Category_CategoryName, T1.Category_Description)
    FROM (
        SELECT 
            T.Id AS Category_Id, 
            T.CategoryName AS Category_CategoryName, 
            T.Description AS Category_Description, 
            True AS _from0
        FROM SmartMarketDB.Categories AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Company.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Company
        [CodeFirstDatabaseSchema.Company](T1.Company_Id, T1.Company_CompanyName, T1.Company_Rate, T1.Company_UserId, T1.Company_ImageURL)
    FROM (
        SELECT 
            T.Id AS Company_Id, 
            T.CompanyName AS Company_CompanyName, 
            T.Rate AS Company_Rate, 
            T.UserId AS Company_UserId, 
            T.ImageURL AS Company_ImageURL, 
            True AS _from0
        FROM SmartMarketDB.Companies AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.User.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView2()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing User
        [CodeFirstDatabaseSchema.User](T1.User_Id, T1.User_UserName, T1.User_Password, T1.User_Active, T1.User_IsSystem, T1.User_Email, T1.User_Address, T1.User_Phone, T1.User_UserType)
    FROM (
        SELECT 
            T.Id AS User_Id, 
            T.UserName AS User_UserName, 
            T.Password AS User_Password, 
            T.Active AS User_Active, 
            T.IsSystem AS User_IsSystem, 
            T.Email AS User_Email, 
            T.Address AS User_Address, 
            T.Phone AS User_Phone, 
            CAST(T.UserType AS [Edm.Int32]) AS User_UserType, 
            True AS _from0
        FROM SmartMarketDB.Users AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Customer.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView3()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Customer
        [CodeFirstDatabaseSchema.Customer](T1.Customer_Id, T1.Customer_Nikename, T1.Customer_Gender, T1.Customer_UserId)
    FROM (
        SELECT 
            T.Id AS Customer_Id, 
            T.Nikename AS Customer_Nikename, 
            CAST(T.Gender AS [Edm.Int32]) AS Customer_Gender, 
            T.UserId AS Customer_UserId, 
            True AS _from0
        FROM SmartMarketDB.Customers AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.OrderItem.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView4()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing OrderItem
        [CodeFirstDatabaseSchema.OrderItem](T1.OrderItem_Id, T1.OrderItem_OrderId, T1.OrderItem_ProductId, T1.OrderItem_Count, T1.OrderItem_PricePerItem)
    FROM (
        SELECT 
            T.Id AS OrderItem_Id, 
            T.OrderId AS OrderItem_OrderId, 
            T.ProductId AS OrderItem_ProductId, 
            T.Count AS OrderItem_Count, 
            T.PricePerItem AS OrderItem_PricePerItem, 
            True AS _from0
        FROM SmartMarketDB.OrderItems AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Order.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView5()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Order
        [CodeFirstDatabaseSchema.Order](T1.Order_Id, T1.Order_CustomerId, T1.Order_OrderDate, T1.Order_IsDelivered, T1.Order_DeliveredDate)
    FROM (
        SELECT 
            T.Id AS Order_Id, 
            T.CustomerId AS Order_CustomerId, 
            T.OrderDate AS Order_OrderDate, 
            T.IsDelivered AS Order_IsDelivered, 
            T.DeliveredDate AS Order_DeliveredDate, 
            True AS _from0
        FROM SmartMarketDB.Orders AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Product.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView6()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Product
        [CodeFirstDatabaseSchema.Product](T1.Product_Id, T1.Product_ProductName, T1.Product_Description, T1.Product_SubCategoryId, T1.Product_CompanyId, T1.Product_Price, T1.Product_AvailableStock, T1.Product_Rate, T1.Product_ImageURL)
    FROM (
        SELECT 
            T.Id AS Product_Id, 
            T.ProductName AS Product_ProductName, 
            T.Description AS Product_Description, 
            T.SubCategoryId AS Product_SubCategoryId, 
            T.CompanyId AS Product_CompanyId, 
            T.Price AS Product_Price, 
            T.AvailableStock AS Product_AvailableStock, 
            T.Rate AS Product_Rate, 
            T.ImageURL AS Product_ImageURL, 
            True AS _from0
        FROM SmartMarketDB.Products AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.SubCategory.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView7()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing SubCategory
        [CodeFirstDatabaseSchema.SubCategory](T1.SubCategory_Id, T1.SubCategory_SubCategoryName, T1.SubCategory_Description, T1.SubCategory_CategoryId)
    FROM (
        SELECT 
            T.Id AS SubCategory_Id, 
            T.SubCategoryName AS SubCategory_SubCategoryName, 
            T.Description AS SubCategory_Description, 
            T.CategoryId AS SubCategory_CategoryId, 
            True AS _from0
        FROM SmartMarketDB.SubCategories AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.Role.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView8()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Role
        [CodeFirstDatabaseSchema.Role](T1.Role_Id, T1.Role_Roles, T1.Role_IsSystem)
    FROM (
        SELECT 
            T.Id AS Role_Id, 
            T.Roles AS Role_Roles, 
            T.IsSystem AS Role_IsSystem, 
            True AS _from0
        FROM SmartMarketDB.Roles AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for CodeFirstDatabase.UserRole.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView9()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing UserRole
        [CodeFirstDatabaseSchema.UserRole](T1.UserRole_Id, T1.UserRole_UserId, T1.UserRole_RoleId)
    FROM (
        SELECT 
            T.Id AS UserRole_Id, 
            T.UserId AS UserRole_UserId, 
            T.RoleId AS UserRole_RoleId, 
            True AS _from0
        FROM SmartMarketDB.UserRoles AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Categories.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView10()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Categories
        [BusinessEntities.Category](T1.Category_Id, T1.Category_CategoryName, T1.Category_Description)
    FROM (
        SELECT 
            T.Id AS Category_Id, 
            T.CategoryName AS Category_CategoryName, 
            T.Description AS Category_Description, 
            True AS _from0
        FROM CodeFirstDatabase.Category AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Companies.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView11()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Companies
        [BusinessEntities.Company](T1.Company_Id, T1.Company_CompanyName, T1.Company_Rate, T1.Company_UserId, T1.Company_ImageURL)
    FROM (
        SELECT 
            T.Id AS Company_Id, 
            T.CompanyName AS Company_CompanyName, 
            T.Rate AS Company_Rate, 
            T.UserId AS Company_UserId, 
            T.ImageURL AS Company_ImageURL, 
            True AS _from0
        FROM CodeFirstDatabase.Company AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Users.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView12()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Users
        [BusinessEntities.User](T1.User_Id, T1.User_UserName, T1.User_Password, T1.User_Active, T1.User_IsSystem, T1.User_Email, T1.User_Address, T1.User_Phone, T1.User_UserType)
    FROM (
        SELECT 
            T.Id AS User_Id, 
            T.UserName AS User_UserName, 
            T.Password AS User_Password, 
            T.Active AS User_Active, 
            T.IsSystem AS User_IsSystem, 
            T.Email AS User_Email, 
            T.Address AS User_Address, 
            T.Phone AS User_Phone, 
            CAST(T.UserType AS [BusinessEntities.UserType]) AS User_UserType, 
            True AS _from0
        FROM CodeFirstDatabase.User AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Customers.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView13()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Customers
        [BusinessEntities.Customer](T1.Customer_Id, T1.Customer_Nikename, T1.Customer_Gender, T1.Customer_UserId)
    FROM (
        SELECT 
            T.Id AS Customer_Id, 
            T.Nikename AS Customer_Nikename, 
            CAST(T.Gender AS [BusinessEntities.Gender]) AS Customer_Gender, 
            T.UserId AS Customer_UserId, 
            True AS _from0
        FROM CodeFirstDatabase.Customer AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.OrderItems.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView14()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing OrderItems
        [BusinessEntities.OrderItem](T1.OrderItem_Id, T1.OrderItem_OrderId, T1.OrderItem_ProductId, T1.OrderItem_Count, T1.OrderItem_PricePerItem)
    FROM (
        SELECT 
            T.Id AS OrderItem_Id, 
            T.OrderId AS OrderItem_OrderId, 
            T.ProductId AS OrderItem_ProductId, 
            T.Count AS OrderItem_Count, 
            T.PricePerItem AS OrderItem_PricePerItem, 
            True AS _from0
        FROM CodeFirstDatabase.OrderItem AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Orders.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView15()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Orders
        [BusinessEntities.Order](T1.Order_Id, T1.Order_CustomerId, T1.Order_OrderDate, T1.Order_IsDelivered, T1.Order_DeliveredDate)
    FROM (
        SELECT 
            T.Id AS Order_Id, 
            T.CustomerId AS Order_CustomerId, 
            T.OrderDate AS Order_OrderDate, 
            T.IsDelivered AS Order_IsDelivered, 
            T.DeliveredDate AS Order_DeliveredDate, 
            True AS _from0
        FROM CodeFirstDatabase.[Order] AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Products.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView16()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Products
        [BusinessEntities.Product](T1.Product_Id, T1.Product_ProductName, T1.Product_Description, T1.Product_SubCategoryId, T1.Product_CompanyId, T1.Product_Price, T1.Product_AvailableStock, T1.Product_Rate, T1.Product_ImageURL)
    FROM (
        SELECT 
            T.Id AS Product_Id, 
            T.ProductName AS Product_ProductName, 
            T.Description AS Product_Description, 
            T.SubCategoryId AS Product_SubCategoryId, 
            T.CompanyId AS Product_CompanyId, 
            T.Price AS Product_Price, 
            T.AvailableStock AS Product_AvailableStock, 
            T.Rate AS Product_Rate, 
            T.ImageURL AS Product_ImageURL, 
            True AS _from0
        FROM CodeFirstDatabase.Product AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.SubCategories.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView17()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing SubCategories
        [BusinessEntities.SubCategory](T1.SubCategory_Id, T1.SubCategory_SubCategoryName, T1.SubCategory_Description, T1.SubCategory_CategoryId)
    FROM (
        SELECT 
            T.Id AS SubCategory_Id, 
            T.SubCategoryName AS SubCategory_SubCategoryName, 
            T.Description AS SubCategory_Description, 
            T.CategoryId AS SubCategory_CategoryId, 
            True AS _from0
        FROM CodeFirstDatabase.SubCategory AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.Roles.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView18()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing Roles
        [BusinessEntities.Role](T1.Role_Id, T1.Role_Roles, T1.Role_IsSystem)
    FROM (
        SELECT 
            T.Id AS Role_Id, 
            T.Roles AS Role_Roles, 
            T.IsSystem AS Role_IsSystem, 
            True AS _from0
        FROM CodeFirstDatabase.Role AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for SmartMarketDB.UserRoles.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView19()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing UserRoles
        [BusinessEntities.UserRole](T1.UserRole_Id, T1.UserRole_UserId, T1.UserRole_RoleId)
    FROM (
        SELECT 
            T.Id AS UserRole_Id, 
            T.UserId AS UserRole_UserId, 
            T.RoleId AS UserRole_RoleId, 
            True AS _from0
        FROM CodeFirstDatabase.UserRole AS T
    ) AS T1");
        }
    }
}

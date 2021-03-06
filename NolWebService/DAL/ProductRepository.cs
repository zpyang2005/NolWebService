﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NOL.Biz;
using ACD.Biz;
using Common.Biz;
using NolWebService.Models;
using AcdAttribute = ACD.Biz.Attribute;

namespace NolWebService.DAL
{
    public class ProductRepository : BaseRepository
    {
        public int GetByCriteria(int categoryID, int subCategoryID, int measureSystem, int sizeCode, int lengthCode, int headCode, int driveCode, 
            int materialCode, int finishCode, int pointCode, int styleCode)
        {
            NOL.Biz.ListItem listItem = new NOL.Biz.ListItem();
            try
            {
                int searchID;
                int subscriberID = 0;
                if (!listItem.SearchByCriteria(subscriberID, categoryID, subCategoryID, measureSystem, sizeCode, lengthCode, headCode, driveCode, 
                    materialCode, finishCode, pointCode, styleCode, out searchID))
                {
                    // Error occurred
                    return -1;
                }
                if (searchID == 0)
                {
                    // No item was found when searching with the criteria. Please try to adjust the criteria and search again.
                    return 0;
                }
                return searchID;
            }
            catch
            {
                return -1;
            }
        }

        public int GetByKeyword(string keyword)
        {
            NOL.Biz.ListItem listItem = new NOL.Biz.ListItem();
            try
            {
                int searchID;
                int subscriberID = 0;
                if (!listItem.SearchByKeyword(subscriberID, keyword, out searchID))
                {
                    // Error occurred
                    return -1;
                }
                if (searchID == 0)
                {
                    // No item was found when searching with the criteria. Please try to adjust the criteria and search again.
                    return 0;
                }
                return searchID;
            }
            catch
            {
                return -1;
            }
        }

        public IEnumerable<Product> GetBySearchID(int searchID)
        {
            List<Product> products = new List<Product>();
            string sql = "select L.ItemID, L.SubscriberID, L.CategoryID, L.PartNumberAbbott, L.AbbottDescription, L.SizeText, L.DescriptionText, L.WebNoteText," +
                " L.Qty, L.Price, L.PriceUOM, L.flgOfferPrice, L.flgCertified, L.flgRoHS, S.SubscriberCode, S.SubscriberName, S.MinOrder, S.MinLine, S.Sales1," +
                " S.Sales2, I.MeasureSystem, I.SizeCode, I.LengthCode, I.LengthValue, I.HeadCode, I.PointCode, I.DriveCode, I.StyleCode," +
                " I.MaterialCode, I.FinishCode, I.Thickness, I.OutDiameter, A.ContactName, A.Street, A.City, A.StateID, A.Province, A.Zip, A.CountryID, A.Phone," +
                " A.Fax, A.Email" +
                " from ListItem L inner join SearchResult R on L.ItemID = R.ItemID inner join Subscriber S on L.SubscriberID = S.SubscriberID" +
                " inner join ItemAttribute I on L.ItemID = I.ItemID inner join AddressBook A on S.SubscriberID = A.SubscriberID where R.SearchID = @SearchID";
            SqlCommand cmd = new SqlCommand(sql, new SqlConnection(nolConnString));
            SqlParameterCollection sqlParams = cmd.Parameters;
            sqlParams.Add(new SqlParameter("@SearchID", SqlDbType.Int)).Value = searchID;
            SupplierRepository supplierRepository = new SupplierRepository();
            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    Product product = BuildProduct(reader);
                    Supplier supplier = supplierRepository.BuildSupplier(reader);
                    ProductAttribute productAttribute = BuildProductAttribute(reader);
                    Address address = supplierRepository.BuildAddress(reader);
                    product.Supplier = supplier;
                    product.Attribute = productAttribute;
                    product.Supplier.Address = address;
                    products.Add(product);
                }
                return products;
            }
            catch
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public Product BuildProduct(SqlDataReader reader)
        {
            Product product = new Product();
            product.ProductID = reader["ItemID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ItemID"]);
            int categoryID = reader["CategoryID"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CategoryID"]);
            product.CategoryName = new ACD.Biz.Category().GetName(categoryID);
            product.PartNumberAbbott = reader["PartNumberAbbott"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PartNumberAbbott"]).Trim();
            product.AbbottDescription = reader["AbbottDescription"] == DBNull.Value ? string.Empty : Convert.ToString(reader["AbbottDescription"]).Trim();
            product.SizeText = reader["SizeText"] == DBNull.Value ? string.Empty : Convert.ToString(reader["SizeText"]).Trim();
            product.DescriptionText = reader["DescriptionText"] == DBNull.Value ? string.Empty : Convert.ToString(reader["DescriptionText"]).Trim();
            product.WebNoteText = reader["WebNoteText"] == DBNull.Value ? string.Empty : Convert.ToString(reader["WebNoteText"]).Trim();
            product.Quantity = reader["Qty"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Qty"]);
            product.Price = reader["Price"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Price"]);
            product.PriceUOM = reader["PriceUOM"] == DBNull.Value ? string.Empty : Convert.ToString(reader["PriceUOM"]).Trim();
            product.IsPriceOffered = reader["flgOfferPrice"] == DBNull.Value ? false : Convert.ToBoolean(reader["flgOfferPrice"]);
            product.Certified = reader["flgCertified"] == DBNull.Value ? false : Convert.ToBoolean(reader["flgCertified"]);
            product.RoHS = reader["flgOfferPrice"] == DBNull.Value ? false : Convert.ToBoolean(reader["flgOfferPrice"]);
            return product;
        }

        public ProductAttribute BuildProductAttribute(SqlDataReader reader)
        {
            int measureSystem = reader["MeasureSystem"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MeasureSystem"]);
            int sizeCode = reader["SizeCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SizeCode"]);
            int lengthCode = reader["LengthCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["LengthCode"]);
            decimal lengthValue = reader["LengthValue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LengthValue"]);
            int headCode = reader["HeadCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["HeadCode"]);
            int pointCode = reader["PointCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PointCode"]);
            int driveCode = reader["DriveCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DriveCode"]);
            int styleCode = reader["StyleCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["StyleCode"]);
            int materialCode = reader["MaterialCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaterialCode"]);
            int finishCode = reader["FinishCode"] == DBNull.Value ? 0 : Convert.ToInt32(reader["FinishCode"]);
            string thickness = reader["Thickness"] == DBNull.Value ? string.Empty : Convert.ToString(reader["Thickness"]).Trim();
            string outDiameter = reader["OutDiameter"] == DBNull.Value ? string.Empty : Convert.ToString(reader["OutDiameter"]).Trim();
            AcdAttribute attribute = new AcdAttribute();
            ProductAttribute pa = new ProductAttribute();
            pa.MeasureSystem = attribute.GetPrintOut(8, measureSystem);
            pa.Size = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Size),  sizeCode);
            pa.Length = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Length), lengthCode);
            pa.LengthValue = lengthValue;
            pa.Head = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Head), headCode);
            pa.Point = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Point), pointCode);
            pa.Drive = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Drive), driveCode);
            pa.Style = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Style), styleCode);
            pa.Material = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Material), materialCode);
            pa.Finish = attribute.GetPrintOut(Convert.ToInt32(DynamicAttribute.Finish), finishCode);
            pa.Thickness= thickness;
            pa.OutDiameter= outDiameter;
            return pa;
        }

    }
}
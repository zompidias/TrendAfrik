using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TABusinessLayer
{
    public class CatalogueBusinessLayer
    {
        string connectionString =
                   ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public IEnumerable<Catalogue> Catalogues
        {
            get
            {


                List<Catalogue> catItems = new List<Catalogue>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllCatalogueType", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Catalogue catitem = new Catalogue();
                        catitem.catalogueId = Convert.ToDecimal(rdr["catalogueId"]);
                        catitem.catalogueName = rdr["catalogueName"].ToString();

                        catItems.Add(catitem);
                    }
                    rdr.Dispose();
                }

                return catItems;
            }
        }
        public IEnumerable<SellerDetails> SellerDetailss
        {
            get
            {


                List<SellerDetails> sellerDetails = new List<SellerDetails>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllSellerDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SellerDetails selleritem = new SellerDetails();
                        selleritem.sellerId = Convert.ToDecimal(rdr["sellerId"]);
                        selleritem.sellerName = rdr["sellerName"].ToString();
                        selleritem.sellerAddress = rdr["sellerAddress"].ToString();
                        selleritem.sellerPhone = rdr["sellerPhone"].ToString();
                        selleritem.sellerEmail = rdr["sellerEmail"].ToString();
                        selleritem.sellerWebsite = rdr["sellerWebsite"].ToString();
                        selleritem.sellerExpiryDate = Convert.ToDateTime(rdr["sellerExpiryDate"].ToString());

                        sellerDetails.Add(selleritem);
                    }
                    rdr.Dispose();
                }

                return sellerDetails;
            }
        }

        public IEnumerable<SellerItems> SellerItemss
        {
            get
            {
                List<SellerItems> sellerItems = new List<SellerItems>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllSellerItems", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SellerItems selleritem = new SellerItems();
                        selleritem.sellerId = Convert.ToDecimal(rdr["sellerId"]);
                        selleritem.catalogueId = Convert.ToDecimal(rdr["catalogueId"]);
                        selleritem.itemDescription = rdr["itemDescription"].ToString();
                        selleritem.itemId = Convert.ToDecimal(rdr["itemId"]);
                        selleritem.itemName = rdr["itemName"].ToString();
                        selleritem.itemPrice = rdr["itemPrice"].ToString();
                        selleritem.itemPicture = rdr["itemPicture"].ToString();
                        selleritem.pictureA = rdr["pictureA"].ToString();
                        selleritem.pictureB = rdr["pictureB"].ToString();
                        selleritem.pictureC = rdr["pictureC"].ToString();
                        selleritem.itemAlternatePicName = rdr["itemAlternatePicName"].ToString();

                        sellerItems.Add(selleritem);
                    }
                    rdr.Dispose();
                }

                return sellerItems;
            }
        }

        public void AddCatalogueToDB(Catalogue collection)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddCatalogue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCatName = new SqlParameter();
                paramCatName.ParameterName = "@CatalogueName";
                paramCatName.Value = collection.catalogueName;
                cmd.Parameters.Add(paramCatName);

                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void AddSellerDetailsToDB(SellerDetails collection)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddSellerDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramsellerAddress = new SqlParameter();
                paramsellerAddress.ParameterName = "@sellerAddress";
                paramsellerAddress.Value = collection.sellerAddress;
                cmd.Parameters.Add(paramsellerAddress);

                SqlParameter paramsellerExpiryDate = new SqlParameter();
                paramsellerExpiryDate.ParameterName = "@sellerExpiryDate";
                paramsellerExpiryDate.Value = collection.sellerExpiryDate;
                cmd.Parameters.Add(paramsellerExpiryDate);

                SqlParameter paramsellerName = new SqlParameter();
                paramsellerName.ParameterName = "@sellerName";
                paramsellerName.Value = collection.sellerName;
                cmd.Parameters.Add(paramsellerName);

                SqlParameter paramsellerPhone = new SqlParameter();
                paramsellerPhone.ParameterName = "@sellerPhone";
                paramsellerPhone.Value = collection.sellerPhone;
                cmd.Parameters.Add(paramsellerPhone);

                SqlParameter paramsellerEmail = new SqlParameter();
                paramsellerEmail.ParameterName = "@sellerEmail";
                paramsellerEmail.Value = collection.sellerEmail;
                cmd.Parameters.Add(paramsellerEmail);

                SqlParameter paramsellerWebsite = new SqlParameter();
                paramsellerWebsite.ParameterName = "@sellerWebsite";
                paramsellerWebsite.Value = collection.sellerWebsite;
                cmd.Parameters.Add(paramsellerWebsite);

                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void AddItemsToDB(SellerItems collection)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddSellerItems", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramitemAlternatePicName = new SqlParameter();
                paramitemAlternatePicName.ParameterName = "@itemAlternatePicName";
                paramitemAlternatePicName.Value = collection.itemAlternatePicName;
                cmd.Parameters.Add(paramitemAlternatePicName);

                SqlParameter paramitemDescription = new SqlParameter();
                paramitemDescription.ParameterName = "@itemDescription";
                paramitemDescription.Value = collection.itemDescription;
                cmd.Parameters.Add(paramitemDescription);

                SqlParameter paramitemName = new SqlParameter();
                paramitemName.ParameterName = "@itemName";
                paramitemName.Value = collection.itemName;
                cmd.Parameters.Add(paramitemName);

                SqlParameter paramitemPicture = new SqlParameter();
                paramitemPicture.ParameterName = "@itemPicture";
                paramitemPicture.Value = collection.itemPicture;
                cmd.Parameters.Add(paramitemPicture);

                SqlParameter paramitemPictureA = new SqlParameter();
                paramitemPictureA.ParameterName = "@pictureA";
                paramitemPictureA.Value = collection.pictureA;
                cmd.Parameters.Add(paramitemPictureA);

                SqlParameter paramitemPictureB = new SqlParameter();
                paramitemPictureB.ParameterName = "@pictureB";
                paramitemPictureB.Value = collection.pictureB;
                cmd.Parameters.Add(paramitemPictureB);

                SqlParameter paramitemPictureC = new SqlParameter();
                paramitemPictureC.ParameterName = "@pictureC";
                paramitemPictureC.Value = collection.pictureC;
                cmd.Parameters.Add(paramitemPictureC);

                SqlParameter paramitemPrice = new SqlParameter();
                paramitemPrice.ParameterName = "@itemPrice";
                paramitemPrice.Value = collection.itemPrice;
                cmd.Parameters.Add(paramitemPrice);

                SqlParameter paramcatalogueId = new SqlParameter();
                paramcatalogueId.ParameterName = "@catalogueId";
                paramcatalogueId.Value = collection.catalogueId;
                cmd.Parameters.Add(paramcatalogueId);

                SqlParameter paramsellerId = new SqlParameter();
                paramsellerId.ParameterName = "@sellerId";
                paramsellerId.Value = collection.sellerId;
                cmd.Parameters.Add(paramsellerId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void SaveChangesCatalogueToDB(Catalogue collection)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveChangesCatalogue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramCatName = new SqlParameter();
                paramCatName.ParameterName = "@catalogueName";
                paramCatName.Value = collection.catalogueName;
                cmd.Parameters.Add(paramCatName);

                SqlParameter paramCatId = new SqlParameter();
                paramCatId.ParameterName = "@catalogueId";
                paramCatId.Value = collection.catalogueId;
                cmd.Parameters.Add(paramCatId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveChangesSellerDetailsToDB(SellerDetails collection)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveChangesSellerDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramsellerAddress = new SqlParameter();
                paramsellerAddress.ParameterName = "@sellerAddress";
                paramsellerAddress.Value = collection.sellerAddress;
                cmd.Parameters.Add(paramsellerAddress);

                SqlParameter paramsellerExpiryDate = new SqlParameter();
                paramsellerExpiryDate.ParameterName = "@sellerExpiryDate";
                paramsellerExpiryDate.Value = collection.sellerExpiryDate;
                cmd.Parameters.Add(paramsellerExpiryDate);

                SqlParameter paramsellerName = new SqlParameter();
                paramsellerName.ParameterName = "@sellerName";
                paramsellerName.Value = collection.sellerName;
                cmd.Parameters.Add(paramsellerName);

                SqlParameter paramsellerPhone = new SqlParameter();
                paramsellerPhone.ParameterName = "@sellerPhone";
                paramsellerPhone.Value = collection.sellerPhone;
                cmd.Parameters.Add(paramsellerPhone);

                SqlParameter paramsellerEmail = new SqlParameter();
                paramsellerEmail.ParameterName = "@sellerEmail";
                paramsellerEmail.Value = collection.sellerEmail;
                cmd.Parameters.Add(paramsellerEmail);

                SqlParameter paramsellerId = new SqlParameter();
                paramsellerId.ParameterName = "@sellerId";
                paramsellerId.Value = collection.sellerId;
                cmd.Parameters.Add(paramsellerId);

                SqlParameter paramsellerWebsite = new SqlParameter();
                paramsellerWebsite.ParameterName = "@sellerWebsite";
                paramsellerWebsite.Value = collection.sellerWebsite;
                cmd.Parameters.Add(paramsellerWebsite);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public SellerItems GetSellerItemsToChange(int id)
        {
            SellerItems selleritem = new SellerItems();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetSellerItemsToChange", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramitemAlternatePicName = new SqlParameter();
                paramitemAlternatePicName.ParameterName = "@id";
                paramitemAlternatePicName.Value = id;
                cmd.Parameters.Add(paramitemAlternatePicName);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    
                    selleritem.sellerId = Convert.ToDecimal(rdr["sellerId"]);
                    selleritem.catalogueId = Convert.ToDecimal(rdr["catalogueId"]);
                    selleritem.itemDescription = rdr["itemDescription"].ToString();
                    selleritem.itemId = Convert.ToDecimal(rdr["itemId"]);
                    selleritem.itemName = rdr["itemName"].ToString();
                    selleritem.itemPrice = rdr["itemPrice"].ToString();
                    selleritem.itemPicture = rdr["itemPicture"].ToString();
                    selleritem.pictureA = rdr["pictureA"].ToString();
                    selleritem.pictureB = rdr["pictureB"].ToString();
                    selleritem.pictureC = rdr["pictureC"].ToString();
                    selleritem.itemAlternatePicName = rdr["itemAlternatePicName"].ToString();

                    //sellerItems.Add(selleritem);
                }
                rdr.Dispose();
            }
            return selleritem;
        }


        public void SaveChangesSellerItemsToDB(SellerItems collection)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveChangesSellerItems", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramitemAlternatePicName = new SqlParameter();
                paramitemAlternatePicName.ParameterName = "@itemAlternatePicName";
                paramitemAlternatePicName.Value = collection.itemAlternatePicName;
                cmd.Parameters.Add(paramitemAlternatePicName);

                SqlParameter paramitemDescription = new SqlParameter();
                paramitemDescription.ParameterName = "@itemDescription";
                paramitemDescription.Value = collection.itemDescription;
                cmd.Parameters.Add(paramitemDescription);

                SqlParameter paramitemName = new SqlParameter();
                paramitemName.ParameterName = "@itemName";
                paramitemName.Value = collection.itemName;
                cmd.Parameters.Add(paramitemName);

                SqlParameter paramitemPicture = new SqlParameter();
                paramitemPicture.ParameterName = "@itemPicture";
                paramitemPicture.Value = collection.itemPicture;
                cmd.Parameters.Add(paramitemPicture);

                SqlParameter paramitemPictureA = new SqlParameter();
                paramitemPictureA.ParameterName = "@pictureA";
                paramitemPictureA.Value = collection.pictureA;
                cmd.Parameters.Add(paramitemPictureA);

                SqlParameter paramitemPictureB = new SqlParameter();
                paramitemPictureB.ParameterName = "@pictureB";
                paramitemPictureB.Value = collection.pictureB;
                cmd.Parameters.Add(paramitemPictureB);

                SqlParameter paramitemPictureC = new SqlParameter();
                paramitemPictureC.ParameterName = "@pictureC";
                paramitemPictureC.Value = collection.pictureC;
                cmd.Parameters.Add(paramitemPictureC);

                SqlParameter paramitemPrice = new SqlParameter();
                paramitemPrice.ParameterName = "@itemPrice";
                paramitemPrice.Value = collection.itemPrice;
                cmd.Parameters.Add(paramitemPrice);

                SqlParameter paramcatalogueId = new SqlParameter();
                paramcatalogueId.ParameterName = "@catalogueId";
                paramcatalogueId.Value = collection.catalogueId;
                cmd.Parameters.Add(paramcatalogueId);

                SqlParameter paramsellerId = new SqlParameter();
                paramsellerId.ParameterName = "@sellerId";
                paramsellerId.Value = collection.sellerId;
                cmd.Parameters.Add(paramsellerId);

                SqlParameter paramItemId = new SqlParameter();
                paramItemId.ParameterName = "@itemId";
                paramItemId.Value = collection.itemId;
                cmd.Parameters.Add(paramItemId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCatalogueFromDB(decimal id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCatalogue", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@itemId";
                paramId.Value = id;
                cmd.Parameters.Add(paramId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<SellerItems> GetSearchRequest(string str)
        {

            List<SellerItems> fooditems = new List<SellerItems>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetSearchFoodItems", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFoodName = new SqlParameter();
                paramFoodName.ParameterName = "@FoodName";
                paramFoodName.Value = str;
                cmd.Parameters.Add(paramFoodName);

                con.Open();
                // cmd.ExecuteNonQuery();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    /* FoodItem fooditem = new FoodItem();
                     fooditem.FoodItemId = Convert.ToDecimal(rdr["FoodItemId"]);
                     fooditem.FoodName = rdr["FoodName"].ToString();
                     fooditem.FoodGroupId = Convert.ToDecimal(rdr["FoodGroupId"]);
                     fooditem.FoodCost = Convert.ToDecimal(rdr["FoodCost"].ToString());
                     fooditem.FoodWeightTypeId = Convert.ToDecimal(rdr["FoodWeightTypeId"]);
                     fooditem.SellerId = Convert.ToDecimal(rdr["SellerId"]);
                     fooditem.QuantityAvailable = Convert.ToInt32(rdr["QuantityAvailable"]);
                     fooditem.SubFoodGroupId = Convert.ToDecimal(rdr["SubFoodGroupId"]);
                     fooditem.FoodPicture = rdr["FoodPicture"].ToString();
                     fooditem.AlternateText = rdr["AlternateText"].ToString();
                     fooditems.Add(fooditem);
                     * */
                }
                rdr.Dispose();

            }

            return fooditems;

        }


        public void AddEnquiryToDB(ContactUs employee)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEnquiryToDB", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFoodName = new SqlParameter();
                paramFoodName.ParameterName = "@Comment";
                paramFoodName.Value = employee.Comment;
                cmd.Parameters.Add(paramFoodName);

                SqlParameter paramFoodGrpId = new SqlParameter();
                paramFoodGrpId.ParameterName = "@Email";
                paramFoodGrpId.Value = employee.Email;
                cmd.Parameters.Add(paramFoodGrpId);

                SqlParameter paramFoodCost = new SqlParameter();
                paramFoodCost.ParameterName = "@FirstName";
                paramFoodCost.Value = employee.FirstName;
                cmd.Parameters.Add(paramFoodCost);

                SqlParameter paramFoodWeigth = new SqlParameter();
                paramFoodWeigth.ParameterName = "@LastName";
                paramFoodWeigth.Value = employee.LastName;
                cmd.Parameters.Add(paramFoodWeigth);

                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public IEnumerable<ItemsToBrowse> RetrieveItemsinCatalogueType(decimal id)
        {

            List<ItemsToBrowse> CatalogueItems = new List<ItemsToBrowse>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetItemsForCatalogueType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFoodName = new SqlParameter();
                paramFoodName.ParameterName = "@catalogueId";
                paramFoodName.Value = id;
                cmd.Parameters.Add(paramFoodName);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ItemsToBrowse CatalogueItem = new ItemsToBrowse();
                    CatalogueItem.sellerId = Convert.ToDecimal(rdr["sellerId"]);
                    CatalogueItem.catalogueId = Convert.ToDecimal(rdr["catalogueId"]);
                    CatalogueItem.itemDescription = rdr["itemDescription"].ToString();
                    CatalogueItem.itemId = Convert.ToDecimal(rdr["itemId"]);
                    CatalogueItem.itemName = rdr["itemName"].ToString();
                    CatalogueItem.itemPrice = rdr["itemPrice"].ToString();
                    CatalogueItem.itemPicture = rdr["itemPicture"].ToString();
                    CatalogueItem.pictureA = rdr["pictureA"].ToString();
                    CatalogueItem.pictureB = rdr["pictureB"].ToString();
                    CatalogueItem.pictureC = rdr["pictureC"].ToString();
                    CatalogueItem.sellerName = rdr["sellerName"].ToString();
                    CatalogueItem.sellerAddress = rdr["sellerAddress"].ToString();
                    CatalogueItem.sellerPhone = rdr["sellerPhone"].ToString();
                    CatalogueItem.sellerEmail = rdr["sellerEmail"].ToString();
                    CatalogueItem.sellerWebsite = rdr["sellerWebsite"].ToString();
                    CatalogueItem.itemAlternatePicName = rdr["itemAlternatePicName"].ToString();

                    CatalogueItems.Add(CatalogueItem);
                }
                rdr.Dispose();
            }

            return CatalogueItems;
        }

        public IEnumerable<ItemsToBrowse> FindSearchRequest(string searchitem)
        {

            List<ItemsToBrowse> CatalogueItems = new List<ItemsToBrowse>();

            var strings = searchitem.Split(' ');

            foreach (var splitString in strings)
            {


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetItemsForSearchRequest", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramFoodName = new SqlParameter();
                    paramFoodName.ParameterName = "@searchItem";
                    paramFoodName.Value = splitString;
                    cmd.Parameters.Add(paramFoodName);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ItemsToBrowse CatalogueItem = new ItemsToBrowse();
                        CatalogueItem.sellerId = Convert.ToDecimal(rdr["sellerId"]);
                        CatalogueItem.catalogueId = Convert.ToDecimal(rdr["catalogueId"]);
                        CatalogueItem.itemDescription = rdr["itemDescription"].ToString();
                        CatalogueItem.itemId = Convert.ToDecimal(rdr["itemId"]);
                        CatalogueItem.itemName = rdr["itemName"].ToString();
                        CatalogueItem.itemPrice = rdr["itemPrice"].ToString();
                        CatalogueItem.itemPicture = rdr["itemPicture"].ToString();
                        CatalogueItem.pictureA = rdr["pictureA"].ToString();
                        CatalogueItem.pictureB = rdr["pictureB"].ToString();
                        CatalogueItem.pictureC = rdr["pictureC"].ToString();
                        CatalogueItem.sellerName = rdr["sellerName"].ToString();
                        CatalogueItem.sellerAddress = rdr["sellerAddress"].ToString();
                        CatalogueItem.sellerPhone = rdr["sellerPhone"].ToString();
                        CatalogueItem.sellerWebsite = rdr["sellerWebsite"].ToString();
                        CatalogueItem.sellerEmail = rdr["sellerEmail"].ToString();
                        CatalogueItem.itemAlternatePicName = rdr["itemAlternatePicName"].ToString();

                        CatalogueItems.Add(CatalogueItem);
                    }
                    rdr.Dispose();
                }
            }
            //remove deuplicates
            /*var MyQuery = (from item in CatalogueItems

                           orderby item

                           select item).Distinct();*/

            var DistinctItems = CatalogueItems.GroupBy(x => x.itemId).Select(y => y.First());

            return DistinctItems;
        }
    }
}

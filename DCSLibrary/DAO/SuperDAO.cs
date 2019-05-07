using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DCSLibrary.DAO
{
    public class SuperDAO
    {
        /// <summary>
        /// Records an <see cref="Order"/>
        /// </summary>
        /// <param name="order"> Order details </param>
        /// <param name="storeName"> Name of the store </param>
        public void RecordLocalOrder(Order order, string storeName)
        {
            if (order.Products == null) { return; }

            SqlConnection cn = new SqlConnection(Properties.Settings.Default.de90ConnectionString);
            SqlCommand cmd;
            string sql = "INSERT INTO Orders (orderId, productId, productCount, paymentMethod, amountPayable, dateOfPurchase, store, customerId, isLocal) ";   //Exclude customerId
            try
            {
                cn.Open();
                cmd = new SqlCommand("SELECT DISTINCT COUNT('orderId') FROM Orders ", cn);
                SqlDataReader reader = cmd.ExecuteReader();
                int newId;
                if (reader.Read())
                {
                    newId = (int)reader[""] + 1;
                    reader.Close();
                }
                else
                    return;
                foreach (KeyValuePair<int, Product> pair in order.Products)
                {   //Each product on new record with its count
                    string sqlB = sql + String.Format("VALUES ( {0}, {1}, {2}, '{3}', '{4}', '{5}', '{6}', {7}, 1) ", newId, pair.Value.Id, pair.Value.NumInStock, order.PaymentMethod, pair.Value.Price, order.DoP, storeName, order.CustomerId, 1);
                    cmd = new SqlCommand(sqlB, cn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error recording order", ex);
            }
            finally
            {
                cn.Close();
            }
        }
        //TODO: Cancel order

        /// <summary>
        /// Gets the products
        /// </summary>
        /// <param name="type"> Category of the products</param>
        /// <returns> A <see cref="Dictionary{int, Product}"/> of products of the requested type.</returns>
        public Dictionary<int, Product> GetListOfProducts(ProductType type)
        {   //TODO much later: filer by store
            //Assume all stores share one inventory
            Dictionary<int, Product> result = new Dictionary<int, Product>();

            string sql = String.Format("SELECT * FROM Products WHERE type = {0}", (int)type);
            SqlConnection cn = new SqlConnection(Properties.Settings.Default.de90ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader r;
            try
            {
                cn.Open();
                r = cmd.ExecuteReader();
                while (r.Read())    //Loop for rows
                {
                    //string imgPath = (r["image"].ToString() == null) ? "" : r["imagePath"].ToString();
                    float price = float.Parse(r["markedPrice"].ToString()) - float.Parse(r["nationalDiscount"].ToString()) + float.Parse(r["localDiscount"].ToString());   //p.originalPrice =... ;
                    Product p = new Product((int)r["productId"], r["name"].ToString(), r["description"].ToString(), (ProductType)r["type"], r["image"].ToString(), price, float.Parse(r["markedPrice"].ToString()), (int)r["minimumAge"])
                    {
                        NumInStock = (int)r["numInStock"]
                    };
                    result.Add(p.Id, p);
                }
                r.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error retrieving products", ex);
            }
            finally
            {
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Adds a new product to inventory
        /// </summary>
        /// <param name="product"> Item to be added</param>
        /// <returns> True if successful, otherwise false </returns>
        public void AddProductToStock(string name, string description, ProductType type, float markedPrice, int minAge)
        {
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.de90ConnectionString);
            string cmd = String.Format("INSERT INTO Products (name, description, type, markedPrice, minimumAge) VALUES ('{0}', '{1}', {2}, '{3}', {4})", name, description, (int)type, markedPrice, minAge);
            SqlCommand command = new SqlCommand(cmd, conn);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("Cannot insert duplicate key"))
                {
                    throw new Exception("A product with the name '" + name + "' already exists");
                }
                else if (e.Message.Contains("onnecting to database"))
                {
                    throw new Exception("Could not connect to database");
                }
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Updates product information
        /// </summary>
        /// <param name="id"> Product id </param>
        /// <param name="product"> New product details </param>
        /// <returns> True if successful else false </returns>
        public bool ModifyProduct(int id, Product p)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.de90ConnectionString);
            string sql = String.Format("UPDATE Products SET name = '{0}', description = '{1}', type = '{2}', image = '{3}', markedPrice = {4}, numInStock = {5} WHERE productId = {6}", p.Name, p.Description, p.Type, p.ImagePath, p.MarkedPrice, p.NumInStock, p.Id);
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (SqlException)
            {
                //TODO: Handle 'key not found' separately
                throw new Exception("Error connecting to database");
            }
            return false;
        }

        //TODO: I am here :)
        public void RegisterPayment(int orderId, Payment payment)
        {
            //SqlConnection connection = new SqlConnection(Properties.Settings.Default.de90ConnectionString);
            //string sql = String.Format("");
            //TODO: reduce product numInStock by
            throw new NotImplementedException();
        }
    }
}


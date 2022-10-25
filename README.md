# APIProjectGroup1
[![Coverage Status](https://coveralls.io/repos/github/WStickler/APIProjectGroup1/badge.svg)](https://coveralls.io/github/WStickler/APIProjectGroup1)

This is the API pproject for group1, which includes Adam Millard, Sergiusz, Syed, William, Ameer and Max.

## **About the Project**
We were tasked with implementing a restful API for the customers table of the Northwind database. We aimed our API at the Customer service, sales and marketing teams for businesses. This is because we feel like they are the most likely to interact with data on customers, be it to access details about customers ordering patterns or to access details about the distribution of customers. 

To accomplish our task, we have introduced a service layer, DTO's, a controller and we are utilising *swagger check ?* to use our API.

## **Using the Api**
To use our API, you will have the opportunity to ...

## **Our Methods**
### **Get Methods:**
For our Get methods we have decided to include an option to search for a specific customer by name/id/?. We have also included the ability for someone to filter and get the results based on city, to allow for marketing teams to have a better understnading of where the may need to target their advertising. By including the orders table we have also made it possible to search for a customer and the get methods will return all orders for that particualr customer allowing for someone to find a particular order quickly. Including this table has also allowed for us to filter the data by numbers of orders a customer has made or when we also include the order details table we can filter by price to see which customers have spent the most money

### **Post Method:**
The post method allows users to add new customers. It has a Customer object parameter, which is passed in from the HTTP request body, in which the CustomerID is required to create the customer, but other details can be added too. A 201 Created status code is returned if successful, otherwise a 409 conflict is returned if they already exist.

### **Put Methods:**
For our Put methods we have decide to include the ability for a customer to update their details in case they change address.

### **Delete Method:**
For our Delete method we have decided to include the option for a user to be able to remove a customer from the database so we no longer store the account details. The CustomerController delete method has a string ID parameter, taken from the HTTP request. It gets the customer using the service layer and deletes the selected customer, returning a status code 204 No Content if successful, or a 404 Not Found if the user can't be located.



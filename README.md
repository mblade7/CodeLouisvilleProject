This project's goal is to allow the user to both figure out their compensation based off of a sale that they did as well as look up products. The user can check their compensation through the "Check Compensation" option and entering in both the sales amount and the gross. The "Search for Product" feature also allows the user to look up a premade product by the ID# (1,2,3,4,5 at the moment) and give details such as Margin Percentage and even the maximum amount a user would make off that product. If for some reason those do not populate, the user can use the "Update Product" feature to recreate the JSON file holding the product items.  <br/><br/>As for features included in the project:
<br/>1. A master loop is used in the UI where it will continue to allow the user to keep using the program over and over until they decide to quit.
<br/>2. The product class implements the interface IEntityWithID (which I've been told can be used as a substitute for the inheritance feature)
<br/>3. "Update Product" creates a JSON file in the bin folder and "Search for Product" uses the information in said JSON file to provide information to the user.
<br/>4. A LINQ Query is used with the GetByID function inside of "SearchforProduct"
<br/>5. The "Check Compensation" option takes information given by the user to convert both of the information into both a margin percentage as well as compensation. This is also done inside of the product class which can be shown to the user from the "Search for Product" option.
<br/><br/><b>Important Note: "Update Product" must be used to create the JSON file when first using program in order for "Search for Product" to work.</b>

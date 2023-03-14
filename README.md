# InventoryManager.MVC  

## An Inventory Manager that helps you manage your Inventory for  your business

### List of things you can
- User Authentication 
- User Update profile and Password
- User Add Product 
- User edit, view and delete Product
- User Add Sales
- User edit, view and delete Sales
- User Generate Balance Sheet (coming soon)

### When App starts a change the connection string in the appsettings.json File. Run the migration then start the app.

### The migration will seed the db with some demo data which you can be able to use to test

` Fullname - Uzumaki Naruto,
            Email - uzumaki.naruto@domain.com,
            Password - 123445678`
    
` Fullname - Sasuke Uchiha,
            Email - sasuke.uchiha@domain.com,
            Password - 6789012345`


### Side Note: After User Auth i was unable to pass userId around because i am still working on implimenting identity so if you use any other user to login that is not user ID 1 (Naruto), like you created a new user or want to use user 2. Go to the ProductController Index action and change the id from 1 to the userID you want to use and build.
# THANK YOU


public sealed class ManageCategories
{ //Using Singleton Pattern
    private static ManageCategories instance = null;
    private static readonly object instanceLock = new object();
    private ManageCategories() { }
    public static ManageCategories Instance
    {
        get{
            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = new ManageCategories();     
                }
                return instance;
            }
        }
    }
    public List<Category> GetCategories()
    {
        List<Category> categories;
        try
        {
            using MyStock stock = new MyStock(); 
            categories = stock.Categories.ToList();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }

        return categories;
    }//end GetCategories
     //---
    public void InsertCategory(Category category)
    {
        try
        {
            using MyStock stock = new MyStock();
            stock.Categories.Add(category);
            stock.SaveChanges();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }

    }//end InsertCategory


    public void UpdateCategory(Category category)
    {
        try
        {
            using MyStock stock = new MyStock();
            stock.Entry<Category>(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            stock.SaveChanges();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }
    }//end UpdateCategory
     //--
    public void DeleteCategory(Category category)
    {
        try
        {
            using MyStock stock = new MyStock();
            //Find Category by CategoryID
            var cate = stock.Categories.SingleOrDefault(c => c.CategoryID == category.CategoryID);
            stock.Categories.Remove(cate);
            stock.SaveChanges();
        }
        catch (Exception ex) {
            throw new Exception(ex.Message);
        }

    }//end DeleteCategory

}//end ManageCategories

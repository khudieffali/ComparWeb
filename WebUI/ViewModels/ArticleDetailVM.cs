using Entities.Concrete;

namespace WebUI.ViewModels
{
    public class ArticleDetailVM
    {
        public Article? Article { get; set; }
        public List<Article>? PopularArticles { get; set; }
        public List<Article>? NewArticles { get; set; }
        public List<Category> Categories { get; set; }

    }
}

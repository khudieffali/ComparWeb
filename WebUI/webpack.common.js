const HtmlWebpackPlugin = require("html-webpack-plugin");

module.exports.shared = [
   "./src/vendors/bootstrap.css",
   "./src/vendors/owl-carousel/assets/owl.carousel.css",
   "./src/vendors/owl-carousel/assets/owl.theme.default.min.css",
   "./src/vendors/pe-icon-7-stroke.css",
   "./src/vendors/icofont.css",
   "./src/vendors/helper.css",
   "./src/js/allStaticScripts.js",
];

module.exports.sharedDynamic = [
   "./src/css/style.scss",
   "./src/js/allScripts.js",
];

const htmlList = [
   "about.html",
   "blog.html",
   "blogDetail.html",
   "contact.html",
   "course.html",
   "faq.html",
   "index.html",
];
module.exports.HtmlWebpackPluginList = htmlList.map((htmlName) => {
   return new HtmlWebpackPlugin({
      filename: `${htmlName}`,
      template: `src/pages/${htmlName}`,
      chunks: ["shared", "sharedDynamic"],
      inject: "body",
      minify: false,
   });
});

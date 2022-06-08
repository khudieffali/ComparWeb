const path = require("path");

const {shared, sharedDynamic, HtmlWebpackPluginList} = require('./webpack.common')

module.exports = (env) => {
   return {
      entry: {
         shared: shared,
         sharedDynamic: sharedDynamic,
      },
      output: {
         path: path.resolve(__dirname, "dist"),
         filename: "[name]/js/[name].bundle.js",
         clean: true,
      },
      
      module: {
         rules: [
            {
               test: /\.s[ac]ss$/,
               use: ["style-loader", "css-loader", "sass-loader"],
            },
            {
               test: /\.css$/i,
               use: ["style-loader", "css-loader"],
            },
            {
               test: /\.(png|svg|jpg|jpeg|gif)$/i,
               type: "asset",
               generator: {
                  filename: "images/[name][ext]",
               },
            },
            {
               test: /\.html$/i,
               loader: "html-loader",
            },
         ],
      },
      
      mode: "development",
      devtool: "source-map",
      devServer: {
         port: 9000,
         hot: true,
         static: "./dist",
         watchFiles: "./src",
      },
      plugins: [...HtmlWebpackPluginList],

     
   };
};
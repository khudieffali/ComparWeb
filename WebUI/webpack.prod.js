const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");
const ImageMinimizerPlugin = require("image-minimizer-webpack-plugin");
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
               test: /\.js$/i,
               exclude: /(node_modules|bower_components)/,
               use: {
                  loader: "babel-loader",
                  options: {
                     presets: ["@babel/preset-env"],
                  },
               },
            },
            {
               test: /\.s[ac]ss$/,
               use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
            },
            {
               test: /\.css$/i,
               use: [MiniCssExtractPlugin.loader, "css-loader"],
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
               options: {
                  minimize: false,
               },
            },
         ],
      },
      mode: "production",
      optimization: {
         minimizer: [
            `...`,
            new CssMinimizerPlugin(),
            new ImageMinimizerPlugin({
               minimizer: {
                  implementation: ImageMinimizerPlugin.imageminMinify,
                  options: {
                     plugins: [
                        "imagemin-gifsicle",
                        "imagemin-mozjpeg",
                        "imagemin-pngquant",
                        "imagemin-svgo",
                     ],
                  },
               },
               loader: false,
            }),
         ],
      },
      plugins: [
         new MiniCssExtractPlugin({
            filename: "[name]/css/[name].css",
            chunkFilename: "[name]/css/[name].css",
         }),
         ...HtmlWebpackPluginList
      ]    
   };
};

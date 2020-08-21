const path = require("path");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  entry: './src/index.tsx',
  plugins: [
    new CleanWebpackPlugin({
      cleanAfterEveryBuildPatterns: ['public/build']
    }),
    new HtmlWebpackPlugin({
      title: "PowerPoint Remote"
    }),
  ],
  output: {
    path: path.join(__dirname + '/public'),
    filename: 'bundle.[contenthash].js'
  },
  resolve: {
    extensions: ['.ts', '.tsx', '.js']
  },
  module: {
    rules: [
      { exclude: /node_modules/, test: /\.tsx?$/, loader: 'ts-loader' }
    ]
  }
}

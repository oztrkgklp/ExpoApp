const path = require('path');

module.exports = {
  mode: 'production',
  target: 'electron-main',
  entry:{
    index: ['babel-polyfill', '../src/main/index.js'],
  },
  output: {
    path: path.join(__dirname, '../dist'),
    filename: 'main.prod.js'
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        loader: 'babel-loader'
      }
    ]
  },
  node: {
    __dirname: false,
    __filename: false
  }
};
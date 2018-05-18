var path = require('path');
//var webpack = require('webpack');

module.exports = {
  entry:
  {
    book: './React/Book/index.js'

  },
  output: {
    path: path.resolve(__dirname, './React/build'),
    filename: '[name].bundle.js',
    library: "pack"
  },
  module: {
    rules: [
    {
      test: /\.js$|\.jsx/,
      exclude: /node_modules/,
      use: {
        loader: "jsx-loader?harmony"
      }
    }
    ]
  },
  resolve: {
    extensions: ['.js', '.jsx']
  },
  externals: {
    // Use external version of React (from CDN for client-side, or
    // bundled with ReactJS.NET for server-side)
    //Dispatcher: 'Dispatcher'
  }
};
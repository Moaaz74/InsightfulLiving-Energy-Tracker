module.exports = function override(config, env) {
    console.log("React app rewired works!")
    config.resolve.fallback = {
      fs:false,
      crypto: false,
      http: require.resolve("stream-http"),
      querystring: false,
      stream: require.resolve("stream-browserify"),
    };
    return config;
  }
const path = require("path");

const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const FileManagerPlugin = require("filemanager-webpack-plugin");
const BundleAnalyzerPlugin = require("webpack-bundle-analyzer").BundleAnalyzerPlugin;

const constants = require("../constants/constants.js");

const WWWROOT_PATH = path.resolve(__dirname, "..", "..", "wwwroot");

const plugins = {
    "fileManager": function initialize(mode) {
        const sourceImagePath = path.resolve(__dirname, "..", "src", "images");
        const destinationImagePath = WWWROOT_PATH + "/images/";

        return new FileManagerPlugin({
            events: {
                onEnd: {
                    delete: [
                        {
                            source: WWWROOT_PATH + "/js/application-styles*.js*",
                            options: {
                                force: true,
                            },
                        }
                    ],
                    copy: [
                        {
                            source: sourceImagePath,
                            destination: destinationImagePath,
                            options: {
                                force: true,
                            },
                        },
                    ]
                }
            }
        });
    },
    "miniCssExtract": function initialize(mode) {
        const filename = mode === constants.environments.production ?
            "css/[name].[contenthash].min.css" :
            "css/[name].css";

        return new MiniCssExtractPlugin({ filename: filename });
    },
    "bundleAnalyzer": function initialize(mode) {
        const stats = mode === constants.environments.production ? "server" : "disabled";

        return new BundleAnalyzerPlugin({
            analyzerMode: stats,
        });
    }
};

module.exports = plugins;

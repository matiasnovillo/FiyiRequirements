/* WEBPACK
 * Webpack can bundle many Js files together with their dependencies in a single Js bundle file.
 * 
 * This Js file has a Node.js module structure and has the main configuration of Webpack library
 * for dev or prod environments.
 */

/* WEBPACK LIBRARIES (Loaders and Plugins) AND FILE TYPES
 * Webpack can bundle different file types if you include the loaders and plugins neccesaries for each one
 * in the package.json file:
 * 
 * Images: url-loader
 * HTML: raw-loader
 * CSS (Dev mode): style-loader and css-loader, 
 * CSS (Prod mode): mini-css-extract-plugin, optimize-css-assets-webpack-plugin and css-loader
 * Js: terser-webpack-plugin
 * Ts: awesome-typescript-loader
 */

/* BOOTSTRAP AND JQUERY
 * Default .NET Core libraries VS. NPM libraries
 * It's easier to use Bootstrap, jQuery and font-awesome from NPM instead
 * of given libraries from .NET Core
 */

/* NOTES ABOUT tsconfig.json
 * compileOnSave: true              Compile Ts files after saving VisualStudio project
 * compilerOptions:                 Compiler configuration/flags
 *   strictNullChecks: true,        true indicate that Ts data types accept null and undefined types
 *   noImplicitAny: false,          false indicate that TODO
 *   noEmitOnError: true,           true to not generate Js files if an error appear in Ts files
 *   removeComments: false,         false to not remove comments
 *   sourceMap: true,               true to generate source maps
 *   target: "es5",                 Ts compiler will output Js files with es5 version
 *   lib: [ "dom", "es2017" ]       Ts compiler will work with Js es2017 version
 * include: ["...","..."...]        Path of folders and files to include in compilation
 * exclude: ["...","..."...]        Path of folders and files to exclude from compilation
 */


/* In Node.js the next lines are nodes where the variables like webpack are objects
 * whose methods come from their respecting libraries
 */
 const webpack = require("webpack");                             
 const path = require("path");                                   
 const SourceMapDevToolPlugin = webpack.SourceMapDevToolPlugin;  //This library allows more control over source map generation
 const bundleOutputDir = "./wwwroot/dist";                       //Virtual path where the bundle will be saved
 const MiniCssExtractPlugin = require("mini-css-extract-plugin");
 const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
 const TerserPlugin = require("terser-webpack-plugin");
 
 module.exports = {
     mode: "production", //development or production
     entry: {
         main: ["./wwwroot/js/site", "./wwwroot/css/site.css"],  //Here we've got common modules used in the entire application

         pagepermissions: ["./wwwroot/ts/CMSCore/PagePermissions"], //Area: BasicCore
         failuretsmodel: ["./wwwroot/ts/BasicCore/Failure/TsModels/Failure_TsModel"],
         failurejquery: ["./wwwroot/ts/BasicCore/Failure/jQuery/FailureQuery_jQuery"],
         parametertsmodel: ["./wwwroot/ts/BasicCore/Parameter/TsModels/Parameter_TsModel"],
         parameterjquery: ["./wwwroot/ts/BasicCore/Parameter/jQuery/ParameterQuery_jQuery"],

         citytsmodel: ["./wwwroot/ts/BasicCulture/City/TsModels/City_TsModel"], //Area: BasicCulture
         cityjquery: ["./wwwroot/ts/BasicCulture/City/jQuery/CityQuery_jQuery"],
         countrytsmodel: ["./wwwroot/ts/BasicCulture/Country/TsModels/Country_TsModel"],
         countryjquery: ["./wwwroot/ts/BasicCulture/Country/jQuery/CountryQuery_jQuery"],
         provincetsmodel: ["./wwwroot/ts/BasicCulture/Province/TsModels/Province_TsModel"],
         provincejquery: ["./wwwroot/ts/BasicCulture/Province/jQuery/ProvinceQuery_jQuery"],
         planettsmodel: ["./wwwroot/ts/BasicCulture/Planet/TsModels/Planet_TsModel"],
         planetjquery: ["./wwwroot/ts/BasicCulture/Planet/jQuery/PlanetQuery_jQuery"],
         sextsmodel: ["./wwwroot/ts/BasicCulture/Sex/TsModels/Sex_TsModel"],
         sexjquery: ["./wwwroot/ts/BasicCulture/Sex/jQuery/SexQuery_jQuery"],

         usertsmodel: ["./wwwroot/ts/CMSCore/User/TsModels/User_TsModel"],  //Area: CMSCore
         userjquery: ["./wwwroot/ts/CMSCore/User/jQuery/UserQuery_jQuery"],
         menutsmodel: ["./wwwroot/ts/CMSCore/Menu/TsModels/Menu_TsModel"],
         menujquery: ["./wwwroot/ts/CMSCore/Menu/jQuery/MenuQuery_jQuery"],
         roletsmodel: ["./wwwroot/ts/CMSCore/Role/TsModels/Role_TsModel"],
         rolejquery: ["./wwwroot/ts/CMSCore/Role/jQuery/RoleQuery_jQuery"],
         rolemenutsmodel: ["./wwwroot/ts/CMSCore/RoleMenu/TsModels/RoleMenu_TsModel"],
         rolemenujquery: ["./wwwroot/ts/CMSCore/RoleMenu/jQuery/RoleMenuQuery_jQuery"],

         applicationtsmodel: ["./wwwroot/ts/Requirement/Application/TsModels/Application_TsModel"],
         applicationjquery: ["./wwwroot/ts/Requirement/Application/jQuery/ApplicationQuery_jQuery"],
         clienttsmodel: ["./wwwroot/ts/Requirement/Client/TsModels/Client_TsModel"],
         clientjquery: ["./wwwroot/ts/Requirement/Client/jQuery/ClientQuery_jQuery"],
         clientapplicationtsmodel: ["./wwwroot/ts/Requirement/ClientApplication/TsModels/ClientApplication_TsModel"],
         clientapplicationjquery: ["./wwwroot/ts/Requirement/ClientApplication/jQuery/ClientApplicationQuery_jQuery"],
         requirementtsmodel: ["./wwwroot/ts/Requirement/Requirement/TsModels/Requirement_TsModel"],
         requirementjquery: ["./wwwroot/ts/Requirement/Requirement/jQuery/RequirementQuery_jQuery"],
         requirementchangehistorytsmodel: ["./wwwroot/ts/Requirement/RequirementChangehistory/TsModels/RequirementChangehistory_TsModel"],
         requirementchangehistoryjquery: ["./wwwroot/ts/Requirement/RequirementChangehistory/jQuery/RequirementChangehistoryQuery_jQuery"],
         requirementfiletsmodel: ["./wwwroot/ts/Requirement/RequirementFile/TsModels/RequirementFile_TsModel"],
         requirementfilejquery: ["./wwwroot/ts/Requirement/RequirementFile/jQuery/RequirementFileQuery_jQuery"],
         requirementnotetsmodel: ["./wwwroot/ts/Requirement/RequirementNote/TsModels/RequirementNote_TsModel"],
         requirementnotejquery: ["./wwwroot/ts/Requirement/RequirementNote/jQuery/RequirementNoteQuery_jQuery"],
         requirementprioritytsmodel: ["./wwwroot/ts/Requirement/RequirementPriority/TsModels/RequirementPriority_TsModel"],
         requirementpriorityjquery: ["./wwwroot/ts/Requirement/RequirementPriority/jQuery/RequirementPriorityQuery_jQuery"]
     },
     output: {
         filename: "[name].bundle.js",                       //Path to save bundles: __dirname + "/wwwroot/dist" (__dirname is a Node.js variable)
         path: path.join(__dirname, bundleOutputDir),
         publicPath: 'dist/'
     },
     //devtool: "source-map",
     resolve: {
         extensions: ['.js', '.ts']                          //File types to work with Webpack
     },
     optimization: {
         minimizer: [
             new TerserPlugin({ test: /\.js(\?.*)?$/i }),
             new OptimizeCSSAssetsPlugin({})
         ]
     },
     performance: {
         hints: false,
         maxEntrypointSize: 51200000,
         maxAssetSize: 51200000
     },
     module: {
         rules: [
             {
                 test: /\.js$/,
                 exclude: ['/node_modules/']
             },
             { test: /\.ts$/, use: 'awesome-typescript-loader?silent=true' },
             { test: /\.html$/, use: 'raw-loader' },
             {
                 test: /\.(png|woff|woff2|eot|ttf|jpg|jpeg|gif|svg)$/,
                 use: 'url-loader?limit=10000'
             },
             {
                 test: /\.css$/,
                 sideEffects: true,
                 use: [
                     {
                         loader: MiniCssExtractPlugin.loader,
                         options: {
                             publicPath: './'
                         }
                     },
                     "css-loader"
                 ]
             },
             {
                 test: /\.less$/,
                 use: [
                     MiniCssExtractPlugin.loader,
                     "css-loader"
                 ]
             }
         ]
     },
     plugins: [
         new webpack.DllReferencePlugin({                    //Reference to the library bundle Manifest. This .json contains all the path modules references
             context: path.join(__dirname,"./wwwroot/dist/"),
             manifest: require('./wwwroot/dist/vendor-manifest.json')
         })].concat([
             new SourceMapDevToolPlugin({
                 filename: '[file].map',
                 moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]')
             }),
             new MiniCssExtractPlugin({
                 filename: "[name].css",
                 chunkFilename: "[id].css"
             })
         ])
 };
// References for external scripts needed to load application

/// <reference path="../jquery-1.8.3.min.js"/> 
/// <reference path="../knockout-2.2.0.debug.js"/>
/// <reference path="../underscore.min.js"/>
/// <reference path="../require.js"/>

// Override require.config function and alter baseUrl when set by Chutzpah
var root = this;
var originalConfig = require.config;

require.config = function (config) {
    if (config.baseUrl !== undefined) {
        config.baseUrl = config.baseUrl.replace(/\/Scripts\/tests.*$/, "/Scripts/app");
    }

    originalConfig.call(require, config);
};

define('jquery', [], function () { return root.jQuery; });
define('ko', [], function () { return root.ko; });
define('underscore', [], function () { return root._; });
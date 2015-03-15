requirejs.config({
    baseUrl: '/',
    paths: {
        underscore: 'Scripts/underscore-min',
        jquery: 'Scripts/jquery-1.10.2.min',
        backbone: 'Scripts/backbone',
        text: 'Scripts/text',
        models: 'Scripts/app/pos/models',
        collections: 'Scripts/app/pos/collections',
        views: 'Scripts/app/pos/views',
        routers: 'Scripts/app/pos/routers',
        components: 'Scripts/app/pos/components'/*,
        modalDialog: 'lib/backbone.ModalDialog/backbone.ModalDialog'*/
    },
    shim: {
        'backbone': {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        'underscore': {
            exports: '_'
        }
    }
});

var app = app || {};

require(['routers/router', 'components/dataService'],
function(router, dataService) {
    $(function() {
        dataService.getData().then(function() {
            router.start();
        });
    });
});
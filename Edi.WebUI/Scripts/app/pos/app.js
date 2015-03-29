requirejs.config({
    baseUrl: '/',
    paths: {
        underscore: 'Scripts/underscore-min',
        jquery: 'Scripts/jquery-1.10.2.min',
        backbone: 'Scripts/backbone',
        text: 'Scripts/text',
        signalr: 'Scripts/jquery.signalR-2.2.0',
        hubs: '/signalr/hubs?noext',
        moment: 'Scripts/moment',
        models: 'Scripts/app/pos/models',
        collections: 'Scripts/app/pos/collections',
        views: 'Scripts/app/pos/views',
        routers: 'Scripts/app/pos/routers',
        components: 'Scripts/app/pos/components'/*,
        modalDialog: 'lib/backbone.ModalDialog/backbone.ModalDialog'*/
    },
    config: {
        moment: {
            noGlobal: true
        }
    },
    shim: {
        'backbone': {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        'underscore': {
            exports: '_'
        },
        'bootstrap': {
            deps: ['jquery']
        }
    }
});

var app = app || {};

require(['routers/router', 'components/dataService', 'moment'],
function(router, dataService, moment) {

    app.moment = moment;
    $(function() {
        dataService.getData().then(function() {
            router.start();
        });
    });
});
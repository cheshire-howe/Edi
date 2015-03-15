define([
    'backbone',
    'models/po'
], function(Backbone, Po) {
    var Pos = Backbone.Collection.extend({
        url: '/api/PurchaseOrderApi',
        model: Po
    });

    return Pos;
});
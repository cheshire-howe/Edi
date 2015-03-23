define([
    'collections/pos'
], function(Pos) {

    function setMaxPoID(pos) {
        app.poID = _.max(pos, function(po) {
            return po.ID;
        }).ID;
    }

    var DataService = {
        getData: function() {
            var deferred = $.Deferred();
            var pos = new Pos();
            pos.fetch().then(function() {
                app.pos = pos;
                deferred.resolve();
            });
            return deferred.promise();
        },

        createPo: function(po) {
            po.url = 'api/PurchaseOrderApi';
            return Backbone.sync('create', po);
        }
    };

    return DataService;
});
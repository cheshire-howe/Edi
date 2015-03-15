define([
    'jquery',
    'underscore',
    'backbone',
    'text!PurchaseOrder/Po'
], function($, _, Backbone, PoTmpl) {
    var poView = Backbone.View.extend({
        template: _.template(PoTmpl),

        render: function() {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        }
    });

    return poView;
});
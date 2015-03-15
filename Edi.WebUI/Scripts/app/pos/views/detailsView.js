define([
    'jquery',
    'underscore',
    'backbone',
    'routers/router',
    'text!PurchaseOrder/PoDetails'
], function($, _, Backbone, Router, DetailTmpl) {
    var poDetailsView = Backbone.View.extend({
        template: _.template(DetailTmpl),
        tagName: 'div',
        events: {
            'click #btnBack': 'back'
        },

        render: function() {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },

        back: function() {
            Backbone.history.history.back();
        }
    });

    return poDetailsView;
});
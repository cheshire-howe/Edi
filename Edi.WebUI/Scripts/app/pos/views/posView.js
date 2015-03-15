define([
    'jquery',
    'underscore',
    'backbone',
    'views/poView',
    'routers/router'],
function($, _, Backbone, PoView, Router) {
    var posView = Backbone.View.extend({
        tagName: 'div',

        initialize: function() {
            this.collection = app.pos;
        },

        render: function() {
            this.$el.html("<h2>Purchase Orders</h2>");
            this.collection.each(function(item) {
                this.addOne(item);
            }, this);
            return this;
        },

        addOne: function(po) {
            var view = new PoView({ model: po });
            this.$el.append(view.render().el);
        }
    });

    return posView;
});
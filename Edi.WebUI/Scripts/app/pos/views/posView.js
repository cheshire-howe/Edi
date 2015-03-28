define([
    'jquery',
    'underscore',
    'backbone',
    'views/poView',
    'routers/router',
    'signalr',
    'hubs'],
function($, _, Backbone, PoView, Router) {
    var posView = Backbone.View.extend({
        tagName: 'div',

        initialize: function() {
            var self = this;
            this.collection = app.pos;

            app.socket = $.connection.poHub;

            app.socket.client.updatePos = function(data) {
                self.collection.fetch({
                    success: function() {
                        self.render();
                    }
                });
            };

            $.connection.hub.start().done(function() {
                app.socket.server.start();
            });
        },

        render: function() {
            this.$el.html("<h2>Purchase Orders</h2>");
            this.$el.append("<p><a href='#/create'>Create Purchase Order</a></p>");
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
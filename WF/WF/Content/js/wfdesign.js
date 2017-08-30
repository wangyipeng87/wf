var wf_view;
function wfnode(options) {
    this.key = "";
    this.text = "";
    this.node = undefined;
    this.nodeType = 2; //1表示开始节点 2表示普通节点 3表示的结束节点
    this.nodeText = undefined;
    this.x = 0;
    this.y = 0;
    this.ox = 0;
    this.oy = 0;
    this.move = function (dx, dy) {
        var x = this.ox + dx;
        var y = this.oy + dy;
        if (x < 0) {
            x = 0;
        }
        else if (x > wf_view.width - this.wfnode.settings.wf_width) {
            x = wf_view.width - this.wfnode.settings.wf_width;
        }

        if (y < 0) {
            y = 0;
        }
        else if (y > wf_view.height - this.wfnode.settings.wf_height) {
            y = wf_view.height - this.wfnode.settings.wf_height;
        }
        this.wfnode.node.attr("x", x);
        this.wfnode.node.attr("y", y);
        if (this.wfnode.nodeText) {
            this.wfnode.nodeText.attr("x", x + 52);
            this.wfnode.nodeText.attr("y", y + 25);
        }
        wf_view.safari();
    };
    this.dragger = function () {
        this.ox = this.attr("x");
        this.oy = this.attr("y");
        this.wfnode.changeStyle();
    };
    this.up = function () {
        this.wfnode.changeStyle();
        //记录移动后的位置 
        var bbox = this.wfnode.node.getBBox();
        if (bbox) {
            this.wfnode.node.position = { "x": bbox.x, "y": bbox.y, "width": bbox.width, "height": bbox.height };
        }
    };
    this.click = function () { };
    this.rightclick = function (e) {
        var cir = e.target;
        layer.alert($(cir).attr("key"));
        return false;
    };
    this.settings = {
        key: "",
        text: "",
        nodeType: 2,
        x: 0,
        y: 0,
        nodeWidth: 108,
        nodeHeight: 50,
        nodeRect: 7,
        noteColor: "#efeff0",
        noteBorderColor: "#23508e",
        opacity: 1,
        strokeWidth: 1,
        cursor: "default",
        fontSize: "12px"
    };

    //改变节点样式
    this.changeStyle = function () {
        if (!this.node) {
            return;
            this.node.attr("fill", this.settings.noteColor);
            this.node.attr("stroke", "#cc0000");
            this.node.animate({ "fill-opacity": .5 }, 500);
        }
    };

    this.settings = $.extend(this.settings, options);
    this.key = this.settings.key;
    if (wf_view == null || wf_view == undefined) {
        wf_view = Raphael("divdesign", $(window).width(), $(window).height() - 28);
    }
    this.node = wf_view.rect(this.settings.x, this.settings.y, this.settings.nodeWidth, this.settings.nodeHeight, this.settings.nodeRect);
    this.node.attr({"fill": this.settings.noteColor, "stroke": this.settings.noteBorderColor, "fill-opacity": this.settings.opacity, "stroke-width": this.settings.strokeWidth, "cursor": this.settings.cursor });
    $(this.node.node).attr("key", this.settings.key);
    $(this.node.node).attr("nodeType", this.settings.nodeType);
    //$(this.node.node).attr("data-target","#context-menu");
    //$(this.node.node).data("nodeType", this.settings.nodeType);
    $(this.node.node).contextmenu({
        target: '#context-menu',
        onItem: function (context, e) {
            //layer.alert($(e.target).text());
            layer.alert($(context).attr("key"));
        }
    });
    this.node.wfnode = this;
    //this.node.node.oncontextmenu = this.rightclick;
    this.node.drag(this.move, this.dragger, this.up);
    this.nodeText = wf_view.text(this.settings.x + 52, this.settings.y + 25, this.settings.text);
    this.nodeText.attr({ "font-size": this.settings.fontSize });
    this.nodeText.attr({ "title": this.settings.text });
};
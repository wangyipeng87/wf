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
            this.wfnode.x = bbox.x;
            this.wfnode.y = bbox.y;
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
        noteBorderColor: "#5DA95E",
        opacity: 1,
        strokeWidth: 1,
        cursor: "pointer",
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
    this.text = this.settings.text;
    this.nodeType = this.settings.nodeType;
    this.x = this.settings.x;
    this.y = this.settings.y;
    if (wf_view == null || wf_view == undefined) {
        wf_view = Raphael("divdesign", $(window).width(), $(window).height() - 28);
    }
    this.node = wf_view.rect(this.settings.x, this.settings.y, this.settings.nodeWidth, this.settings.nodeHeight, this.settings.nodeRect);
    this.node.attr({ "fill": this.settings.noteColor, "stroke": this.settings.noteBorderColor, "fill-opacity": this.settings.opacity, "stroke-width": this.settings.strokeWidth, "cursor": this.settings.cursor });
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

function wfrule(options) {
    this.key = "";
    this.text = "";
    this.beginNodeKey = "";
    this.endNodeKey = "";
    this.expression = "";
    this.beginNode = undefined;
    this.endNode = undefined;
    this.ruleText = undefined;
    this.beginCircle = undefined;
    this.endCircle = undefined;
    this.rulePath = undefined;
    this.beginx = 0;
    this.beginy = 0;
    this.endx = 0;
    this.endy = 0;
    this.settings = {
        key: "",
        text: "",
        beginNodeKey: "",
        endNodeKey: "",
        expression: "",
        beginx: 0,
        beginy: 0,
        endx: 0,
        endy: 0,
        arrWidth: 15,
        pathWidth: 2,
        circleWidth: 7,
        ruleColor: "#5DA95E",
        opacity: 1,
        cursor: "pointer",
        fontSize: "12px",
        backgroundcolor: "#efeff0"
    };

    this.updatexy = function (bx, by, ex, ey) {
        var npath = getArrForString(bx, by, ex, ey, this.settings.arrWidth);
        $(this.rulePath.node).attr("d", npath);
        $(this.beginCircle.node).attr("cx", bx);
        $(this.beginCircle.node).attr("cy", by);
        $(this.endCircle.node).attr("cx", ex);
        $(this.endCircle.node).attr("cy", ey);
        $(this.ruleText.node).attr("x", (bx + ex) / 2);
        $(this.ruleText.node).attr("y", (by + ey) / 2);
        this.beginx = bx;
        this.beginy = by;
        this.endx = ex;
        this.endy = ey;
    },
    this.pathmove = function (dx, dy) {
        var bx = this.obeginx + dx;
        var by = this.obeginy + dy;
        var ex = this.oendx + dx;
        var ey = this.oendy + dy;
        if (bx < 0) {
            bx = 0;
        }
        if (by < 0) {
            by = 0;
        }
        if (ex < 0) {
            ex = 0;
        }
        if (ey < 0) {
            ey = 0;
        }
        this.rule.updatexy(bx, by, ex, ey);
        wf_view.safari();
    };
    this.pathdragger = function () {
        this.obeginx = this.rule.beginx;
        this.obeginy = this.rule.beginy;
        this.oendx = this.rule.endx;
        this.oendy = this.rule.endy;
    };
    this.pathup = function () {
    };
    this.pathclick = function () { };
    this.pathrightclick = function (e) {
        var cir = e.target;
        layer.alert($(cir).attr("key"));
        return false;
    };

    this.beginmove = function (dx, dy) {
        var bx = this.obeginx + dx;
        var by = this.obeginy + dy;
        var ex = this.oendx;
        var ey = this.oendy ;
        if (bx < 0) {
            bx = 0;
        }
        if (by < 0) {
            by = 0;
        }
        if (ex < 0) {
            ex = 0;
        }
        if (ey < 0) {
            ey = 0;
        }
        this.rule.updatexy(bx, by, ex, ey);
        wf_view.safari();
    };
    this.begindragger = function () {
        this.obeginx = this.rule.beginx;
        this.obeginy = this.rule.beginy;
        this.oendx = this.rule.endx;
        this.oendy = this.rule.endy;
    };
    this.beginup = function () {
    };
    this.beginclick = function () { };
    this.beginrightclick = function (e) {
        var cir = e.target;
        layer.alert($(cir).attr("key"));
        return false;
    };


    this.endmove = function (dx, dy) {
        var bx = this.obeginx ;
        var by = this.obeginy ;
        var ex = this.oendx + dx;
        var ey = this.oendy + dy;
        if (bx < 0) {
            bx = 0;
        }
        if (by < 0) {
            by = 0;
        }
        if (ex < 0) {
            ex = 0;
        }
        if (ey < 0) {
            ey = 0;
        }
        this.rule.updatexy(bx, by, ex, ey);
        wf_view.safari();
    };
    this.enddragger = function () {
        this.obeginx = this.rule.beginx;
        this.obeginy = this.rule.beginy;
        this.oendx = this.rule.endx;
        this.oendy = this.rule.endy;
    };
    this.endup = function () {

    };
    this.endclick = function () { };
    this.endrightclick = function (e) {
        var cir = e.target;
        layer.alert($(cir).attr("key"));
        return false;
    };
    this.settings = $.extend(this.settings, options);
    this.key = this.settings.key;
    this.text = this.settings.text;
    this.expression = this.settings.expression;
    this.beginNodeKey = this.settings.beginNodeKey;
    this.endNodeKey = this.settings.endNodeKey;
    this.beginx = this.settings.beginx;
    this.beginy = this.settings.beginy;
    this.endx = this.settings.endx;
    this.endy = this.settings.endy;
    if (wf_view == null || wf_view == undefined) {
        wf_view = Raphael("divdesign", $(window).width(), $(window).height() - 28);
    }

    this.rulePath = wf_view.path(getArr(this.settings.beginx, this.settings.beginy, this.settings.endx, this.settings.endy, this.settings.arrWidth));
    this.rulePath.attr({ "stroke-width": this.settings.pathWidth, "stroke": this.settings.ruleColor, "cursor": this.settings.cursor });
    $(this.rulePath.node).attr("key", this.settings.key);
    $(this.rulePath.node).contextmenu({
        target: '#context-menu',
        onItem: function (context, e) {
            //layer.alert($(e.target).text());
            layer.alert($(context).attr("key")+"线");
        }
    });
    this.rulePath.drag(this.pathmove, this.pathdragger, this.pathup);
    this.rulePath.rule = this;
    this.rulePath.settings = this.settings;

    this.beginCircle = wf_view.circle(this.settings.beginx, this.settings.beginy, this.settings.circleWidth);
    this.beginCircle.attr({ "fill": this.settings.ruleColor, "stroke": this.settings.ruleColor, "cursor": this.settings.cursor });
    $(this.beginCircle.node).attr("key", this.settings.key);
    $(this.beginCircle.node).contextmenu({
        target: '#context-menu',
        onItem: function (context, e) {
            //layer.alert($(e.target).text());
            layer.alert($(context).attr("key") + "起点");
        }
    });
    this.beginCircle.drag(this.beginmove, this.begindragger, this.beginup);
    this.beginCircle.rule = this;


    this.endCircle = wf_view.circle(this.settings.endx, this.settings.endy, this.settings.circleWidth);
    this.endCircle.attr({ "fill": this.settings.ruleColor, "stroke": this.settings.ruleColor, "cursor": this.settings.cursor });
    $(this.endCircle.node).attr("key", this.settings.key);
    $(this.endCircle.node).contextmenu({
        target: '#context-menu',
        onItem: function (context, e) {
            //layer.alert($(e.target).text());
            layer.alert($(context).attr("key")+"终点");
        }
    });
    this.endCircle.drag(this.endmove, this.enddragger, this.endup);
    this.endCircle.rule = this;
    this.ruleText = wf_view.text((this.settings.beginx + this.settings.endx) / 2, (this.settings.beginy + this.settings.endy) / 2, this.settings.text);
    this.ruleText.attr({ "font-size": this.settings.fontSize, "flood-color": this.settings.backgroundcolor, "cursor": this.settings.cursor });
    this.ruleText.attr({ "title": this.settings.text });


}
function getArr(x1, y1, x2, y2, size) {
    var angle = Raphael.angle(x1, y1, x2, y2);
    var a45 = Raphael.rad(angle - 45);
    var a45m = Raphael.rad(angle + 45);
    var x2a = x2 + Math.cos(a45) * size;
    var y2a = y2 + Math.sin(a45) * size;
    var x2b = x2 + Math.cos(a45m) * size;
    var y2b = y2 + Math.sin(a45m) * size;
    var result = ["M", x1, y1, "L", x2, y2, "L", x2a, y2a, "M", x2, y2, "L", x2b, y2b];
    return result;
}

function getArrForString(x1, y1, x2, y2, size) {
    var angle = Raphael.angle(x1, y1, x2, y2);
    var a45 = Raphael.rad(angle - 45);
    var a45m = Raphael.rad(angle + 45);
    var x2a = x2 + Math.cos(a45) * size;
    var y2a = y2 + Math.sin(a45) * size;
    var x2b = x2 + Math.cos(a45m) * size;
    var y2b = y2 + Math.sin(a45m) * size;
    var result = "M"+x1+" "+y1+" L"+x2+" "+y2+" L"+ x2a+" "+ y2a+ " M"+ x2+" "+ y2+ " L"+ x2b+" "+ y2b;
    return result;
}
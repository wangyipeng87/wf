var wf_view;
var nodelist = new Array();
var rulelist = new Array();
function wfnode(options) {
    this.key = "";
    this.text = "";
    this.node = undefined;
    this.nodeType = 3; //1表示开始节点 2表示的结束节点 3表示普通节点
    this.nodeText = undefined;
    this.nodeDescription = "";
    this.x = 0;
    this.y = 0;
    this.ox = 0;
    this.oy = 0;
    this.ProcessType = -1;
    this.ProcessTypeValue = "";
    this.ExecType = 1;
    this.TimeLimit = 0;
    this.URL = "";
    this.IsGoBack = 0;
    this.GoBackType = "";
    this.userlist = new Array();
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
        this.wfnode.x = x;
        this.wfnode.y = y;

        if (rulelist != null && rulelist != undefined && rulelist.length > 0) {
            for (var i = 0; i < rulelist.length; i++) {
                if (rulelist[i].beginNodeKey == this.wfnode.key) {
                    var beginpoint = getnodeline(this.wfnode, rulelist[i]);
                    rulelist[i].updatexy(beginpoint.x, beginpoint.y, rulelist[i].endx, rulelist[i].endy);
                    if (rulelist[i].endNode != null && rulelist[i].endNode != undefined) {
                        var endpoint = getnodeline(rulelist[i].endNode, rulelist[i]);
                        rulelist[i].updatexy(rulelist[i].beginx, rulelist[i].beginy, endpoint.x, endpoint.y);
                    }
                }

                if (rulelist[i].endNodeKey == this.wfnode.key) {
                    var endpoint = getnodeline(this.wfnode, rulelist[i]);
                    rulelist[i].updatexy(rulelist[i].beginx, rulelist[i].beginy, endpoint.x, endpoint.y);
                    if (rulelist[i].beginNode != null && rulelist[i].beginNode != undefined) {
                        var beginpoint = getnodeline(rulelist[i].beginNode, rulelist[i]);
                        rulelist[i].updatexy(beginpoint.x, beginpoint.y, rulelist[i].endx, rulelist[i].endy);
                    }
                }
            }
        }
        wf_view.safari();
    };
    this.dragger = function () {
        this.ox = this.attr("x");
        this.oy = this.attr("y");
        this.wfnode.changeStyle();
    };
    this.up = function () {
        //this.wfnode.changeStyle();
        ////记录移动后的位置 
        //var bbox = this.wfnode.node.getBBox();
        //if (bbox) {
        //    this.wfnode.node.position = { "x": bbox.x, "y": bbox.y, "width": bbox.width, "height": bbox.height };
        //    this.wfnode.x = bbox.x;
        //    this.wfnode.y = bbox.y;
        //}
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
        nodeType: 3,
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
        fontSize: "12px",
        nodeDescription: "",
        ProcessType: -1,
        ProcessTypeValue: "",
        ExecType: 1,
        TimeLimit: 0,
        URL: "",
        IsGoBack: 0,
        GoBackType: "",
        userlist: undefined
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
    this.ProcessType = this.settings.ProcessType;
    this.ProcessTypeValue = this.settings.ProcessTypeValue;
    this.x = this.settings.x;
    this.y = this.settings.y;
    this.ExecType = this.settings.ExecType;
    this.TimeLimit = this.settings.TimeLimit;
    this.nodeDescription = this.settings.nodeDescription;
    this.URL = this.settings.URL;
    this.IsGoBack = this.settings.IsGoBack;
    this.GoBackType = this.settings.GoBackType;
    this.userlist = this.settings.userlist;
    if (wf_view == null || wf_view == undefined) {
        wf_view = Raphael("divdesign", $(window).width(), $(window).height() - 28);
    }
    this.node = wf_view.rect(this.settings.x, this.settings.y, this.settings.nodeWidth, this.settings.nodeHeight, this.settings.nodeRect);
    this.node.attr({ "fill": this.settings.noteColor, "stroke": this.settings.noteBorderColor, "fill-opacity": this.settings.opacity, "stroke-width": this.settings.strokeWidth, "cursor": this.settings.cursor });
    $(this.node.node).attr("key", this.settings.key);
    $(this.node.node).attr("nodeType", this.settings.nodeType);
    //$(this.node.node).attr("data-target","#context-menu");
    //$(this.node.node).data("nodeType", this.settings.nodeType);
    if (this.settings.nodeType == 3) {
        $(this.node.node).contextmenu({
            target: '#context-menu',
            onItem: function (context, e) {
                if ($(e.target).text() == "删除") {
                    layer.confirm('您确定要删除该节点吗？', {
                        btn: ['确定', '取消'] //按钮
                    }, function () {
                        removeNode($(context).attr("key"));
                        layer.closeAll();
                    }, function () {
                        layer.closeAll();
                    });
                }
                if ($(e.target).text() == "属性") {
                    NodeSet($(context).attr("key"));
                }
            }
        });
    }
    this.node.wfnode = this;
    //this.node.node.oncontextmenu = this.rightclick;
    this.node.drag(this.move, this.dragger, this.up);
    this.nodeText = wf_view.text(this.settings.x + 52, this.settings.y + 25, this.settings.text);
    this.nodeText.attr({ "font-size": this.settings.fontSize });
    this.nodeText.attr({ "title": this.settings.text });
    this.updateState = function (nodeoperation) {
        var nodesetting = {
            key: "",
            text: "",
            nodeType: 3,
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
            fontSize: "12px",
            nodeDescription: "",
            ProcessType: -1,
            ProcessTypeValue: "",
            ExecType: 1,
            TimeLimit: 0,
            URL: "",
            IsGoBack: 0,
            GoBackType: "",
            userlist: undefined
        };
        nodesetting = $.extend(nodesetting, nodeoperation);
        if (rulelist != null && rulelist != undefined && rulelist.length > 0) {
            for (var i = 0; i < rulelist.length; i++) {
                if (rulelist[i].beginNodeKey == this.key) {
                    rulelist[i].beginNodeKey = nodesetting.key;
                }
                if (rulelist[i].endNodeKey == this.key) {
                    rulelist[i].endNodeKey = nodesetting.key;
                }
            }
        }
        this.key = nodesetting.key;
        this.text = nodesetting.text;
        this.nodeType = nodesetting.nodeType;
        this.ProcessType = nodesetting.ProcessType;
        this.ProcessTypeValue = nodesetting.ProcessTypeValue;
        this.ExecType = nodesetting.ExecType;
        this.TimeLimit = nodesetting.TimeLimit;
        this.nodeDescription = nodesetting.nodeDescription;
        this.URL = nodesetting.URL;
        this.IsGoBack = nodesetting.IsGoBack;
        this.GoBackType = nodesetting.GoBackType;
        this.userlist = nodesetting.userlist;
        $(this.node.node).attr("key", nodesetting.key);
        this.nodeText.attr({ "text": nodesetting.text });
    }
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
        //$(this.rulePath.node).attr("d", npath);
        //$(this.beginCircle.node).attr("cx", bx);
        //$(this.beginCircle.node).attr("cy", by);
        //$(this.endCircle.node).attr("cx", ex);
        //$(this.endCircle.node).attr("cy", ey);
        //$(this.ruleText.node).attr("x", (bx + ex) / 2);
        //$(this.ruleText.node).attr("y", (by + ey) / 2);
        this.rulePath.attr("path", npath);
        this.beginCircle.attr("cx", bx);
        this.beginCircle.attr("cy", by);
        this.endCircle.attr("cx", ex);
        this.endCircle.attr("cy", ey);
        this.ruleText.attr("x", (bx + ex) / 2);
        this.ruleText.attr("y", (by + ey) / 2);
        this.beginx = bx;
        this.beginy = by;
        this.endx = ex;
        this.endy = ey;
    },
    this.pathmove = function (dx, dy) {
        if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
            return;
        }
        if (this.rule.endNode != null && this.rule.endNode != undefined) {
            return;
        }
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
        if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
            return;
        }
        if (this.rule.endNode != null && this.rule.endNode != undefined) {
            return;
        }
        this.obeginx = this.rule.beginx;
        this.obeginy = this.rule.beginy;
        this.oendx = this.rule.endx;
        this.oendy = this.rule.endy;
    };
    this.pathup = function () {
        if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
            return;
        }
        if (this.rule.endNode != null && this.rule.endNode != undefined) {
            return;
        }
        if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
            for (var i = 0; i < nodelist.length; i++) {
                if (nodeIncludePoint(nodelist[i], { x: this.rule.beginx, y: this.rule.beginy })) {
                    this.rule.beginNodeKey = nodelist[i].key;
                    this.rule.beginNode = nodelist[i];
                    var beginpoint = getnodeline(nodelist[i], this.rule);
                    this.rule.updatexy(beginpoint.x, beginpoint.y, this.rule.endx, this.rule.endy);
                    if (this.rule.endNode != null && this.rule.endNode != undefined) {
                        var endpoint = getnodeline(this.rule.endNode, this.rule);
                        this.rule.updatexy(this.rule.beginx, this.rule.beginy, endpoint.x, endpoint.y);
                    }
                    break;
                }
            }
        }
        if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
            for (var i = 0; i < nodelist.length; i++) {
                if (nodeIncludePoint(nodelist[i], { x: this.rule.endx, y: this.rule.endy })) {
                    this.rule.endNodeKey = nodelist[i].key;
                    this.rule.endNode = nodelist[i];
                    var endpoint = getnodeline(nodelist[i], this.rule);
                    this.rule.updatexy(this.rule.beginx, this.rule.beginy, endpoint.x, endpoint.y);
                    if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
                        var beginpoint = getnodeline(this.rule.beginNode, this.rule);
                        this.rule.updatexy(beginpoint.x, beginpoint.y, this.rule.endx, this.rule.endy);
                    }
                    break;
                }
            }
        }
        wf_view.safari();
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
        var ey = this.oendy;
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
        if (this.rule.endNode != null && this.rule.endNode != undefined) {
            var endpoint = getnodeline(this.rule.endNode, this.rule);
            this.rule.updatexy(this.rule.beginx, this.rule.beginy, endpoint.x, endpoint.y);
        }
        wf_view.safari();
    };
    this.begindragger = function () {
        this.obeginx = this.rule.beginx;
        this.obeginy = this.rule.beginy;
        this.oendx = this.rule.endx;
        this.oendy = this.rule.endy;
    };
    this.beginup = function () {
        var isbegin = false;
        if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
            for (var i = 0; i < nodelist.length; i++) {
                if (nodeIncludePoint(nodelist[i], { x: this.rule.beginx, y: this.rule.beginy })) {
                    isbegin = true;
                    this.rule.beginNodeKey = nodelist[i].key;
                    this.rule.beginNode = nodelist[i];
                    var beginpoint = getnodeline(nodelist[i], this.rule);
                    this.rule.updatexy(beginpoint.x, beginpoint.y, this.rule.endx, this.rule.endy);
                    if (this.rule.endNode != null && this.rule.endNode != undefined) {
                        var endpoint = getnodeline(this.rule.endNode, this.rule);
                        this.rule.updatexy(this.rule.beginx, this.rule.beginy, endpoint.x, endpoint.y);
                    }
                    break;
                }
            }
        }
        if (!isbegin) {
            this.rule.beginNodeKey = "";
            this.rule.beginNode = undefined;
            if (this.rule.endNode != null && this.rule.endNode != undefined) {
                var endpoint = getnodeline(this.rule.endNode, this.rule);
                this.rule.updatexy(this.rule.beginx, this.rule.beginy, endpoint.x, endpoint.y);
            }
        }
        wf_view.safari();
    };
    this.beginclick = function () { };
    this.beginrightclick = function (e) {
        var cir = e.target;
        layer.alert($(cir).attr("key"));
        return false;
    };


    this.endmove = function (dx, dy) {
        var bx = this.obeginx;
        var by = this.obeginy;
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
        if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
            var beginpoint = getnodeline(this.rule.beginNode, this.rule);
            this.rule.updatexy(beginpoint.x, beginpoint.y, this.rule.endx, this.rule.endy);
        }
        wf_view.safari();
    };
    this.enddragger = function () {
        this.obeginx = this.rule.beginx;
        this.obeginy = this.rule.beginy;
        this.oendx = this.rule.endx;
        this.oendy = this.rule.endy;
    };
    this.endup = function () {
        var isend = false;
        if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
            for (var i = 0; i < nodelist.length; i++) {
                if (nodeIncludePoint(nodelist[i], { x: this.rule.endx, y: this.rule.endy })) {
                    isend = true;
                    this.rule.endNodeKey = nodelist[i].key;
                    this.rule.endNode = nodelist[i];
                    var endpoint = getnodeline(nodelist[i], this.rule);
                    this.rule.updatexy(this.rule.beginx, this.rule.beginy, endpoint.x, endpoint.y);
                    if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
                        var beginpoint = getnodeline(this.rule.beginNode, this.rule);
                        this.rule.updatexy(beginpoint.x, beginpoint.y, this.rule.endx, this.rule.endy);
                    }
                    break;
                }
            }
        }

        if (!isend) {
            this.rule.endNodeKey = "";
            this.rule.endNode = undefined;
            if (this.rule.beginNode != null && this.rule.beginNode != undefined) {
                var beginpoint = getnodeline(this.rule.beginNode, this.rule);
                this.rule.updatexy(beginpoint.x, beginpoint.y, this.rule.endx, this.rule.endy);
            }
        }
        wf_view.safari();
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
            if ($(e.target).text() == "删除") {
                layer.confirm('您确定要删除该规则吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    removeRule($(context).attr("key"));
                    layer.closeAll();
                }, function () {
                    layer.closeAll();
                });
            }

            if ($(e.target).text() == "属性") {
                RuleSet($(context).attr("key"));
            }

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
            if ($(e.target).text() == "删除") {
                layer.confirm('您确定要删除该规则吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    removeRule($(context).attr("key"));
                    layer.closeAll();
                }, function () {
                    layer.closeAll();
                });
            }
            if ($(e.target).text() == "属性") {
                RuleSet($(context).attr("key"));
            }
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
            if ($(e.target).text() == "删除") {
                layer.confirm('您确定要删除该规则吗？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    removeRule($(context).attr("key"));
                    layer.closeAll();
                }, function () {
                    layer.closeAll();
                });
            }
            if ($(e.target).text() == "属性") {
                RuleSet($(context).attr("key"));
            }
        }
    });
    this.endCircle.drag(this.endmove, this.enddragger, this.endup);
    this.endCircle.rule = this;
    this.ruleText = wf_view.text((this.settings.beginx + this.settings.endx) / 2, (this.settings.beginy + this.settings.endy) / 2, this.settings.text);
    this.ruleText.attr({ "font-size": this.settings.fontSize, "flood-color": this.settings.backgroundcolor, "cursor": this.settings.cursor });
    this.ruleText.attr({ "text": this.settings.text });

    this.updatestate = function (ruleoperation) {
        var rulesettings = {
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
        rulesettings = $.extend(rulesettings, ruleoperation);
        this.key = rulesettings.key;
        this.text = rulesettings.text;
        this.expression = rulesettings.expression;
        this.ruleText.attr({ "text": rulesettings.text });
        $(this.endCircle.node).attr("key", rulesettings.key);
        $(this.beginCircle.node).attr("key", rulesettings.key);
        $(this.rulePath.node).attr("key", rulesettings.key);
    };
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
    var result = "M" + x1 + " " + y1 + " L" + x2 + " " + y2 + " L" + x2a + " " + y2a + " M" + x2 + " " + y2 + " L" + x2b + " " + y2b;
    return result;
}
//求规则和节点的交点
function getnodeline(wnode, wrule) {
    var x = wnode.x;
    var y = wnode.y;
    var height = wnode.settings.nodeHeight;
    var width = wnode.settings.nodeWidth;
    var beginx = wrule.beginx;
    var beginy = wrule.beginy;
    if (wrule.beginNode != null && wrule.beginNode != undefined) {
        beginx = wrule.beginNode.x + wrule.beginNode.settings.nodeWidth / 2;
        beginy = wrule.beginNode.y + wrule.beginNode.settings.nodeHeight / 2;
    }
    var endx = wrule.endx
    var endy = wrule.endy
    if (wrule.endNode != null && wrule.endNode != undefined) {
        endx = wrule.endNode.x + wrule.endNode.settings.nodeWidth / 2;
        endy = wrule.endNode.y + wrule.endNode.settings.nodeHeight / 2;
    }
    var point = undefined;
    //左
    if (segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x, y: y }, { x: x, y: y + height })) {
        point = segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x, y: y }, { x: x, y: y + height });
        return point;
    }
    //下
    if (segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x, y: y + height }, { x: x + width, y: y + height })) {
        point = segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x, y: y + height }, { x: x + width, y: y + height });
        return point;
    }
    //右
    if (segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x + width, y: y + height }, { x: x + width, y: y })) {
        point = segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x + width, y: y + height }, { x: x + width, y: y });
        return point;
    }
    //上
    if (segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x + width, y: y }, { x: x, y: y })) {
        point = segmentsIntr({ x: beginx, y: beginy }, { x: endx, y: endy }, { x: x + width, y: y }, { x: x, y: y });
        return point;
    }
    if (!point) {
        if (wrule.beginNode != null && wrule.beginNode != undefined && wrule.beginNode.key == wnode.key) {
            point = { x: beginx, y: beginy };
        }
        if (wrule.endNode != null && wrule.endNode != undefined && wrule.endNode.key == wnode.key) {
            point = { x: endx, y: endy };
        }
    }
    return point;
}
//求规则和节点的交点
function nodeIncludePoint(wnode, point) {
    var x = wnode.x;
    var y = wnode.y;
    var height = wnode.settings.nodeHeight;
    var width = wnode.settings.nodeWidth;
    var px = point.x;
    var py = point.y;
    if (px >= x && px <= x + width && py >= y && py <= y + height) {
        return true;
    }
    return false;;
}
/*求两条线段的交点
用法：segmentsIntr({x:0,y:0},{x:100,y:100},{x:20,y:20},{x:150,y:50})
返回值 ：flase 为没有交点 {x,y}为有交点
*/
function segmentsIntr(a, b, c, d) {
    /** 1 解线性方程组, 求线段交点. **/
    // 如果分母为0 则平行或共线, 不相交  
    var denominator = (b.y - a.y) * (d.x - c.x) - (a.x - b.x) * (c.y - d.y);
    if (denominator == 0) {
        return false;
    }

    // 线段所在直线的交点坐标 (x , y)      
    var x = ((b.x - a.x) * (d.x - c.x) * (c.y - a.y)
                + (b.y - a.y) * (d.x - c.x) * a.x
                - (d.y - c.y) * (b.x - a.x) * c.x) / denominator;
    var y = -((b.y - a.y) * (d.y - c.y) * (c.x - a.x)
                + (b.x - a.x) * (d.y - c.y) * a.y
                - (d.x - c.x) * (b.y - a.y) * c.y) / denominator;

    /** 2 判断交点是否在两条线段上 **/
    if (
        // 交点在线段1上  
        (x - a.x) * (x - b.x) <= 0 && (y - a.y) * (y - b.y) <= 0
        // 且交点也在线段2上  
         && (x - c.x) * (x - d.x) <= 0 && (y - c.y) * (y - d.y) <= 0
        ) {

        // 返回交点p  
        return {
            x: x,
            y: y
        }
    }
    //否则不相交  
    return false

}
function removeNode(key) {
    if (rulelist != null && rulelist != undefined && rulelist.length > 0) {
        for (var i = 0; i < rulelist.length; i++) {
            if (rulelist[i].beginNodeKey == key) {
                rulelist[i].beginNodeKey = "";
                rulelist[i].beginNode = undefined;
            }
            if (rulelist[i].endNodeKey == key) {
                rulelist[i].endNodeKey = "";
                rulelist[i].endNode = undefined;
            }
        }
    }
    if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
        var j = -1;
        for (var i = 0; i < nodelist.length; i++) {
            if (nodelist[i].key == key) {
                j = i;
                nodelist[i].node.remove();
                nodelist[i].nodeText.remove();
                break;
            }
        }
        nodelist.splice(j, 1);
    }
}

function removeRule(key) {
    if (rulelist != null && rulelist != undefined && rulelist.length > 0) {
        var j = -1;
        for (var i = 0; i < rulelist.length; i++) {
            if (rulelist[i].key == key) {
                j = i;
                rulelist[i].beginCircle.remove();
                rulelist[i].endCircle.remove();
                rulelist[i].ruleText.remove();
                rulelist[i].rulePath.remove();
                break;
            }
        }
        rulelist.splice(j, 1);
    }
}
function getNodeByKey(key) {
    if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
        var j = -1;
        for (var i = 0; i < nodelist.length; i++) {
            if (nodelist[i].key == key) {
                return nodelist[i];
            }
        }
    }
}
function getRuleByKey(key) {
    if (rulelist != null && rulelist != undefined && rulelist.length > 0) {
        var j = -1;
        for (var i = 0; i < rulelist.length; i++) {
            if (rulelist[i].key == key) {
                return rulelist[i];
            }
        }
    }
}
function addNode() {
    nodelist.push(new wfnode({
        key: Raphael.createUUID(),
        text: "流程" + nodelist.length,
        nodeType: 3,
        x: 200,
        y: 20,
        nodeWidth: 108,
        nodeHeight: 50,
        nodeRect: 7,
        noteColor: "#efeff0",
        noteBorderColor: "#5DA95E",
        opacity: 0.8,
        strokeWidth: 1.5,
        cursor: "pointer"
    }));
}
function addRule() {
    rulelist.push(new wfrule({
        key: Raphael.createUUID(),
        text: "",
        beginNodeKey: "",
        endNodeKey: "",
        expression: "",
        beginx: 20,
        beginy: 20,
        endx: 60,
        endy: 70,
        arrWidth: 15,
        pathWidth: 3,
        circleWidth: 4,
        ruleColor: "#5DA95E",
        opacity: 0.8,
        cursor: "pointer",
        fontSize: "12px"
    }));
}
$(document).ready(function () {
    wf_view = Raphael("divdesign", $(window).width(), $(window).height() - 28);

    nodelist.push(new wfnode({
        key: Raphael.createUUID(),
        text: "申请",
        nodeType: 1,
        x: 200,
        y: 20,
        nodeWidth: 108,
        nodeHeight: 50,
        nodeRect: 7,
        noteColor: "#5DA95E",
        noteBorderColor: "#5DA95E",
        opacity: 0.8,
        strokeWidth: 1.5,
        cursor: "pointer"
    }));
    nodelist.push(new wfnode({
        key: Raphael.createUUID(),
        text: "结束",
        nodeType: 2,
        x: 200,
        y: 200,
        nodeWidth: 108,
        nodeHeight: 50,
        nodeRect: 7,
        noteColor: "#FF0000",
        noteBorderColor: "#5DA95E",
        opacity: 0.8,
        strokeWidth: 1.5,
        cursor: "pointer"
    }));
});

function NodeSet(key) {
    layer.open({
        type: 2,
        title: '流程节点设置 ',
        shadeClose: true,
        shade: 0.8,
        area: ['95%', '90%'],
        content: '../Flow/NodeDetail?key=' + key //iframe的url
    });
}
function RuleSet(key) {
    layer.open({
        type: 2,
        title: '流程规则设置 ',
        shadeClose: true,
        shade: 0.8,
        area: ['95%', '90%'],
        content: '../Flow/RuleDetail?key=' + key //iframe的url
    });
}


function getjson() {
    var tmpjson = {
        tmpkey: $("#hidkey").val(),
        rulelist: new Array(),
        nodelist: new Array()
    };
    if (nodelist != null && nodelist != undefined && nodelist.length > 0) {
        for (var i = 0; i < nodelist.length; i++) {
            var nod = {
                Tmpkey: tmpjson.tmpkey,
                Nodekey: nodelist[i].key,
                NodeName: nodelist[i].text,
                Description: nodelist[i].nodeDescription,
                ProcessType: nodelist[i].ProcessType,
                ProcessTypeValue: nodelist[i].ProcessTypeValue,
                ExecType: nodelist[i].ExecType,
                TimeLimit: nodelist[i].TimeLimit,
                NodeType: nodelist[i].nodeType,
                URL: nodelist[i].URL,
                IsGoBack: nodelist[i].IsGoBack,
                GoBackType: nodelist[i].GoBackType,
                x: nodelist[i].x,
                y: nodelist[i].y,
                userlist: new Array()
            }
            if (nodelist[i].userlist != null && nodelist[i].userlist != undefined && nodelist[i].userlist.length > 0) {
                for (var j = 0; j < nodelist[i].userlist.length; j++) {
                    nod.userlist.push({
                        UserName: nodelist[i].userlist[j].username,
                        UserCode: nodelist[i].userlist[j].usercode
                    });
                }
            }
            tmpjson.nodelist.push(nod);
        }
    }
    if (rulelist != null && rulelist != undefined && rulelist.length > 0) {
        for (var i = 0; i < rulelist.length; i++) {
            tmpjson.rulelist.push({
                Tmpkey: tmpjson.tmpkey,
                Rulekey: rulelist[i].key,
                BeginNodeKey: rulelist[i].beginNodeKey,
                EndNodekey: rulelist[i].endNodeKey,
                Expression: rulelist[i].expression,
                Description: rulelist[i].text,
                BeginX: rulelist[i].beginx,
                BeginY: rulelist[i].beginy,
                EndX: rulelist[i].endx,
                EndY: rulelist[i].endy
            });
        }
    }
    return tmpjson;
}
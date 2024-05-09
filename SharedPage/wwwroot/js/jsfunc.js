window.aaa = { sdf: 'asdf' };

export class JsFunc {
    static liChart = {};
    static SelectList = {};
    static ResizeListener = {};
    static init(id) {
        try {
            var ele = document.getElementById(id);
            if (ele) {
                var myChart = echarts.init(ele);
                if (myChart) {
                    JsFunc.liChart[id] = myChart;

                    console.log('初始化成功', JsFunc.liChart, id);
                }
                else {
                    console.log('初始化失败', JsFunc.liChart, id);
                }
            }
        } catch (e) {
            console.log('初始化失败', e);
        }

    }
    static setOption(id, option, notMerge, lazyUpdate) {
        try {
            var chart = JsFunc.liChart[id];
            if (chart) {
                //chart.showLoading(); 
                //chart.hideLoading();
                let opt = eval('(' + option + ')');
                chart.setOption(opt, notMerge, lazyUpdate);
                //  chart.setOption(option);
            }
            else {
                console.log('没有这个chart实例', id);
            }
        } catch (e) {
            console.log(e);
        }
    }
    static dispose(id) {
        try {
            var chart = JsFunc.liChart[id];
            if (chart) {
                if (chart.isDisposed()) {
                    chart.dispose();//测试永远不会走到这里,不知道是哪里执行了销毁
                }
                else {
                    //console.log("已经被销毁了");
                }
                delete JsFunc.liChart[id];
            }
            else {
                //console.log('已经被销毁了', id);
            }
        } catch (e) {

        }
    }
    static resize(id) {
        try {
            if (JsFunc.liChart[id]) {
                JsFunc.liChart[id].resize();
            }
        } catch (e) {
            console.log('resize失败', e);
        }

    }
    static addResizeListener(id) {
        JsFunc.ResizeListener[id] = () => { JsFunc.liChart[id].resize(); };
        window.addEventListener("resize", JsFunc.ResizeListener[id]);

    }
    static removeResizeListener(id) {
        window.removeEventListener("resize", JsFunc.ResizeListener[id]);

    }
    static addClassForSelect(id, isCtrl) {
        if (!isCtrl) {
            JsFunc.removeClassForSelect();
        }
        var ele = document.getElementById(id);
        if (ele && !ele.classList.contains('selected')) {
            JsFunc.SelectList[id] = id;
            ele.classList.add('selected');
        }
    }
    static removeClassForSelect() {
        for (var id in JsFunc.SelectList) {
            var ele = document.getElementById(id);
            if (ele) {
                if (ele.classList.contains('selected')) {
                    ele.classList.remove('selected');
                }
            }
        }
    }
    static getWH(id) {
        var w = document.getElementById(id).clientWidth;
        var h = document.getElementById(id).clientHeight;
        return [w, h]
    }
    static isFunction(str) {
        try {
            //var func = new Function('return ' + str);
            //if (typeof func === 'function') {
            //    return true;
            //} else {
            //    return false;
            //}
            new Function(str);
            return true;
        } catch (e) {
            return false;
        }
    }

    static Log(id) {
        console.log(id);
    }

}
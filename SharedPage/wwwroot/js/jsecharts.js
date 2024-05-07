
export class echartsFunc {
    static liChart = {};
    static ResizeListener = {};
    static init(id) {
        try {
            var ele = document.getElementById(id);
            if (ele) {
                var myChart = echarts.init(ele);
                if (myChart) {
                    echartsFunc.liChart[id] = myChart;
                    console.log('初始化成功', echartsFunc.liChart,id);
                }
                else {
                    console.log('初始化失败', echartsFunc.liChart,id);
                }
            }
        } catch (e) {
            console.log('初始化失败',e);
        }

    }
    static setOption(id, option, notMerge, lazyUpdate) {
        try {
            var chart = echartsFunc.liChart[id];
            if (chart) {
                let opt = eval('(' + option + ')');
                chart.setOption(opt);
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
            var chart = echartsFunc.liChart[id];
            if (chart) {
                if (chart.isDisposed()) {
                    chart.dispose();//测试永远不会走到这里,不知道是哪里执行了销魂
                }
                else {
                    //console.log("已经被销毁了");
                }
                delete echartsFunc.liChart[id];
            }
            else {
                //console.log('已经被销毁了', id);
            }
        } catch (e) {

        }
    }
    static resize(id) {
        try {
            if (echartsFunc.liChart[id]) {
                echartsFunc.liChart[id].resize();
            }
        } catch (e) {
            console.log('resize失败', e);
        }
        
    }
    static addResizeListener(id) {
        echartsFunc.ResizeListener[id] = () => { echartsFunc.liChart[id].resize(); };
        window.addEventListener("resize", echartsFunc.ResizeListener[id]);
     
    }
    static removeResizeListener(id) {
        window.removeEventListener("resize", echartsFunc.ResizeListener[id]);

    }
    static addClassForSelect(id) {
        var ele = document.getElementById(id);
        if (!ele.classList.contains('selected')) {
            ele.classList.add('selected');
        }
    }
    static removeClassForSelect() {
        try {
            for (var id in echartsFunc.liChart) {
                console.log(id);
                var ele = document.getElementById(id);
                if (ele) {
                    if (ele.classList.contains('selected')) {
                        ele.classList.remove('selected');
                    }
                }
            }
        }
        catch (e) {
            console.log(e);
            console.log('cuocuole');
        }


    }
    static getWH(id) {
        var w = document.getElementById(id).clientWidth;
        var h = document.getElementById(id).clientHeight;
        return [w, h]
    }
    static Log(id) {
        console.log(id);
    }

}
﻿let formDesigner = {
    highlihter: {
        node: null
    },
    utils: {}
};

//
formDesigner.highlihter.activate = (event, cssClassName) => {
    let currentNode = event.target;
    while (currentNode.parentNode) {
        if (/\w-element/.test(currentNode.className)) {
            //如果为element元素,则进行添加边框
            if (formDesigner.highlihter.node === currentNode) {
                return;
            }
            if (formDesigner.highlihter.node) {
                formDesigner.highlihter.node.className = formDesigner.highlihter.node.className.replace(cssClassName, '').trim();
            }
            currentNode.className += ' ' + cssClassName;
            formDesigner.highlihter.node = currentNode;
            return;
        }
        currentNode = currentNode.parentNode;
    }
};

formDesigner.highlihter.deactivate = (event, cssClassName) => {
    if (formDesigner.highlihter.node) {
        //移除边框样式
        formDesigner.highlihter.node.className = formDesigner.highlihter.node.className.replace(cssClassName, '');
        formDesigner.highlihter.node = null;
    }
};
//为一个元素增加类名
formDesigner.utils.addClass = (element, classList) => {
    let classArr = classList.split(" ");
    for (let i = 0; i < classArr.length; i++) {
        element.classList.add(classArr[i]);
    }
}
//为一个元素移除类名
formDesigner.utils.removeClass = function (element, classList) {
    let classArr = classList.split(" ");
    for (let i = 0; i < classArr.length; i++) {
        element.classList.remove(classArr[i]);
    }
}
//移除拖动添加控件的样式
formDesigner.utils.removeClassById = function (elementId, classList) {
    let element = document.getElementById(elementId);
    if (element === null || element === {} || element === undefined) {
        console.error("Element is not found. Element Id: " + elementId);
        return;
    }
    formDesigner.utils.removeClass(element, classList);
};

window.formDesigner = formDesigner;
window.downLoadLarge = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}
window.downLoadSmall = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}

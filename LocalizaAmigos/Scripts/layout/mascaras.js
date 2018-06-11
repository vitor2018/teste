function Mascara(o, f) {
    vObj = o;
    vFun = f;
    setTimeout("exec()", 1);
}

function exec() {
    vObj.value = vFun(vObj.value);
}

function cep(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/^(\d{5})(\d)/, "$1-$2");
    return v;
}
import{_ as S}from"./DataLayout-2LVpN9Ff.js";import{f as p,o as m,c as l,d as o,u as t,w as i,F as C,Z as M,k as A,e as $,t as b,b as v}from"./app-BqYB4w7h.js";import B from"./AgentFilters-BCuO1Udf.js";import L from"./AgentResults-DUBV5GRC.js";import{_ as N}from"./DataPanel-eTawuoZh.js";import{C as P}from"./Code-DkVlEmhY.js";import{_ as V}from"./ScrollToTop-jtdKJjCK.js";import{u as E}from"./LoadAndShowItems-CdGcY1M4.js";import{u as F}from"./StoreSelector-eCG8qd9a.js";import"./AuthenticatedLayout-DmkbqqwL.js";import"./TopBar-CSycyVTE.js";import"./ApplicationLogo-CbRF8qXe.js";import"./form-BD1T68Go.js";import"./use-resolve-button-type-zbaL4VAc.js";import"./calculate-active-index-CJPhqQS-.js";import"./use-text-value-5zAQL0RU.js";import"./use-tree-walker-CEHhLUFs.js";import"./Select-I1X3nauV.js";import"./NeutralBadge-Cx27iNjU.js";import"./_plugin-vue_export-helper-DlAUqK2U.js";import"./ChevronUpDownIcon-BxOM5Ujt.js";import"./LinkButton-wwdNtJLC.js";import"./AgentField-Cckzi0Yd.js";import"./TextInputWithCompletion-u-xIqChF.js";import"./InputError-_zuJ5dV-.js";import"./disposables-CuHKeC00.js";import"./InputLabel-B012P3ON.js";import"./PrimaryButton-JrsBxlP3.js";import"./FilterItems-DtKLW4qf.js";import"./XapiProps-BVq1ckif.js";import"./moment-Cl4UOzQZ.js";import"./transition-CSxLRhS2.js";import"./ChevronUpIcon-CDU9mihh.js";import"./LoadItems-f7CjgfCr.js";const T={key:1,class:"text-gray-800 dark:text-white/90 text-center pt-12"},yt={__name:"Agents",setup(j){const{currentStore:d}=F(),a=p({agent:null,type:"all"}),u=p({location:"all"}),{loadItems:c,loadMoreItems:f,loadingItems:_,loadedCounter:g,loadedItems:n,loadingErrors:I,hasMoreItems:h,showItem:w,itemPanelVisible:r,itemContent:y,itemContentType:x,itemIndex:k}=E({url:"/trax/api/gateway/front/stores/"+d()+"/agents",filters:a,baseParams:{options:{join_members:!0}},settings:{sortProp:"stored",more:!0}});return(D,e)=>(m(),l(C,null,[o(t(M),{title:"Agents"}),o(S,{"side-menu-current":"Agents"},{default:i(()=>[o(N,{show:t(r),"onUpdate:show":e[0]||(e[0]=s=>A(r)?r.value=s:null)},{title:i(()=>[$(b(t(k)+1),1)]),content:i(()=>[o(P,{data:t(y),type:t(x)},null,8,["data","type"])]),_:1},8,["show"]),o(B,{filters:a,options:u,counter:t(g),errors:t(I),onApply:e[1]||(e[1]=s=>t(c)(s))},null,8,["filters","options","counter","errors"]),t(n).length?(m(),v(L,{key:0,loadedItems:t(n),hasMoreItems:t(h),loadingItems:t(_),onLoadMore:t(f),onShow:t(w),class:"mt-8"},null,8,["loadedItems","hasMoreItems","loadingItems","onLoadMore","onShow"])):(m(),l("div",T," No agent or group found! ")),o(V)]),_:1})],64))}};export{yt as default};

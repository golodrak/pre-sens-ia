(function(){
  const sets = {
    ux: ['heroTitleUx','heroDescUx','badgesUx','featuresUx'],
    compliance: ['heroTitleComp','heroDescComp','badgesComp','featuresComp']
  };
  const buttons = document.querySelectorAll('[data-tab]');

  function show(tab){
    ['heroTitleUx','heroDescUx','badgesUx','featuresUx','heroTitleComp','heroDescComp','badgesComp','featuresComp']
      .forEach(id => document.getElementById(id)?.classList.add('d-none'));
    sets[tab].forEach(id => document.getElementById(id)?.classList.remove('d-none'));
    buttons.forEach(b => b.classList.toggle('btn-light', b.dataset.tab===tab));
  }

  buttons.forEach(b => b.addEventListener('click', () => show(b.dataset.tab)));
  show('ux');
})();
// Theme toggle
(function(){
  const key='presensia-theme';
  const btn=document.getElementById('themeToggle');
  function apply(t){ document.documentElement.classList.remove('theme-light','theme-dark'); if(t) document.documentElement.classList.add('theme-'+t); }
  let cur=localStorage.getItem(key);
  apply(cur);
  btn?.addEventListener('click',()=>{cur = cur==='dark'?'light':(cur==='light'?null:'dark'); if(cur) localStorage.setItem(key,cur); else localStorage.removeItem(key); apply(cur);});
})();

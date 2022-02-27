Browser's ...

1. localStorage 
 is scoped to the browser's window. 
 If the user reloads the page or closes and re-opens the browser, the state persists.
 If the user opens multiple browser tabs, the state is shared across the tabs. Data persists in localStorage until explicitly cleared.

2.sessionStorage 
 is scoped to the browser tab. 
 If the user reloads the tab, the state persists. 
 If the user closes the tab or the browser, the state is lost. 
 If the user opens multiple browser tabs, each tab has its own independent version of the data.


ServerPrerendered: 
  The component is rendered to HTML in the response, and then connects back to the server when run in the browser to 
  provide the interactivity.
Server: 
  The component isn't rendered in the HTML, instead a marker is rendered that connects back to your app when run in the browser.
Static: 
  The component is rendered to static HTML in the response, and that's it, done.
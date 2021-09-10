import React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter } from "react-router-dom";

// core styles
import "./src/scss/volt.scss";

// vendor styles
// import "@fortawesome/fontawesome-free/css/all.css";
import "react-datetime/css/react-datetime.css";

import HomePage from "./src/pages/HomePage";
import ScrollToTop from "./src/components/ScrollToTop";

ReactDOM.render(
  <HashRouter>
    <ScrollToTop />
    <HomePage />
  </HashRouter>,
  document.getElementById("root")
);

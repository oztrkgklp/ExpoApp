import React, { useState, useEffect } from 'react';
import { Route, Switch, Redirect } from "react-router-dom";
import { Routes } from "../routes";

// pages
import DashboardOverview from "./dashboard/DashboardOverview";
import BootstrapTables from "./tables/BootstrapTables";
import Signin from "./examples/Signin";
import Lock from "../components/Lock";
import NotFoundPage from "./examples/NotFound";
import ServerError from "./examples/ServerError";
import Signup from "./examples/Signup";
// import {AccomomdationTable} from "../components/AccomomdationTable"
// components
import Sidebar from "../components/Sidebar";
import Navbar from "../components/Navbar";
import Footer from "../components/Footer";
import Preloader from "../components/Preloader";
import {CompanyTable, GuestsTable, NoPurchaseCompany, NotAttendTable, PageVisitsTable} from "../components/Tables"
import DailyAccommodations from '../components/DailyAccommodations';
import Proforma from '../components/Proforma';
import ExternalTableDetail from '../components/ExternalTableDetail';
import Accommodation from '../components/Accommodation';
const RouteWithLoader = ({ component: Component, ...rest }) => {
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    const timer = setTimeout(() => setLoaded(true), 1000);
    return () => clearTimeout(timer);
  }, []);

  return (
    <Route {...rest} render={props => ( <> <Preloader show={loaded ? false : true} /> <Component {...props} /> </> ) } />
  );
};

const RouteWithSidebar = ({ component: Component, ...rest }) => {
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    const timer = setTimeout(() => setLoaded(true), 1000);
    return () => clearTimeout(timer);
  }, []);

  const localStorageIsSettingsVisible = () => {
    return localStorage.getItem('settingsVisible') === 'false' ? false : true
  }

  const [showSettings, setShowSettings] = useState(localStorageIsSettingsVisible);

  const toggleSettings = () => {
    setShowSettings(!showSettings);
    localStorage.setItem('settingsVisible', !showSettings);
  }
 
  return (
    <Route {...rest} render={props => (
      <>
        <Preloader show={loaded ? false : true} />
        <Sidebar />
        
        <main className="content">
          <Navbar />
          <Component {...props} />
          <Footer toggleSettings={toggleSettings} showSettings={showSettings} />
        </main>
      </>
    )}
    />
  );
};

export default () => (
  
  <Switch>
    <RouteWithLoader exact path={Routes.Presentation.path} component={Signin} />
    <RouteWithLoader exact path={Routes.Lock.path} component={Lock} />
    <RouteWithLoader exact path={Routes.NotFound.path} component={NotFoundPage} />
    <RouteWithLoader exact path={Routes.ServerError.path} component={ServerError} />

    {/* pages */}
    <RouteWithSidebar exact path={Routes.DashboardOverview.path} component={DashboardOverview} />
    <RouteWithSidebar exact path={Routes.BootstrapTables.path} component={BootstrapTables} />
    <RouteWithSidebar exact path={Routes.NewCompany.path} component={Signup} />
    <RouteWithSidebar exact path={Routes.Acconmodation.path} component={Accommodation} />
    <RouteWithSidebar exact path={Routes.CompanyTable.path} component={CompanyTable} />
    <RouteWithSidebar exact path={Routes.GuestTable.path} component={GuestsTable} />
    <RouteWithSidebar exact path={Routes.NoAttendTable.path} component={NoPurchaseCompany} />
    <RouteWithSidebar exact path={Routes.SellTaable.path} component={PageVisitsTable} />
    <RouteWithSidebar exact path={Routes.DailyAccommodations.path} component={DailyAccommodations} />
    <RouteWithSidebar exact path={Routes.Proforma.path} component={Proforma} />
    <RouteWithSidebar exact path={Routes.ExternalDetail.path} component={ExternalTableDetail} />



    <Redirect to={Routes.NotFound.path} />
  </Switch>
);

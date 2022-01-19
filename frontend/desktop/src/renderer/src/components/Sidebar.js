import React, { useState } from "react";
import SimpleBar from "simplebar-react";
import { useLocation } from "react-router-dom";
import { CSSTransition } from "react-transition-group";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUsers,
  faBed,
  faChartPie,
  faCog,
  faFileAlt,
  faHandHoldingUsd,
  faSignOutAlt,
  faTable,
  faTimes,
  faCalendarAlt,
  faMapPin,
  faInbox,
  faRocket,
  faInfoCircle,
  faMoneyCheckAlt,
  faCreditCard,
  faExternalLinkSquareAlt,
} from "@fortawesome/free-solid-svg-icons";
import {
  Card,
  Col,
  Nav,
  Badge,
  Image,
  Button,
  Dropdown,
  Accordion,
  Navbar,
} from "@themesberg/react-bootstrap";
import { Link } from "react-router-dom";

import { Routes } from "../routes";
import ThemesbergLogo from "../assets/img/themesberg.svg";
import ReactHero from "../assets/img/technologies/react-hero-logo.svg";
import ProfilePicture from "../assets/img/team/profile-picture-3.jpg";

export default (props = {}) => {
  const location = useLocation();
  const { pathname } = location;
  const [show, setShow] = useState(false);
  const showClass = show ? "show" : "";

  const onCollapse = () => setShow(!show);

  const CollapsableNavItem = (props) => {
    const { eventKey, title, icon, children = null } = props;
    const defaultKey = pathname.indexOf(eventKey) !== -1 ? eventKey : "";

    return (
      <Accordion as={Nav.Item} defaultActiveKey={defaultKey}>
        <Accordion.Item eventKey={eventKey}>
          <Accordion.Button
            as={Nav.Link}
            className="d-flex justify-content-between align-items-center"
          >
            <span>
              <span className="sidebar-icon">
                <FontAwesomeIcon icon={icon} />{" "}
              </span>
              <span className="sidebar-text">{title}</span>
            </span>
          </Accordion.Button>
          <Accordion.Body className="multi-level">
            <Nav className="flex-column">{children}</Nav>
          </Accordion.Body>
        </Accordion.Item>
      </Accordion>
    );
  };

  const NavItem = (props) => {
    const {
      title,
      link,
      external,
      target,
      icon,
      image,
      badgeText,
      badgeBg = "secondary",
      badgeColor = "primary",
    } = props;
    const classNames = badgeText
      ? "d-flex justify-content-start align-items-center justify-content-between"
      : "";
    const navItemClassName = link === pathname ? "active" : "";
    const linkProps = external ? { href: link } : { as: Link, to: link };

    return (
      <Nav.Item className={navItemClassName} onClick={() => setShow(false)}>
        <Nav.Link {...linkProps} target={target} className={classNames}>
          <span>
            {icon ? (
              <span className="sidebar-icon">
                <FontAwesomeIcon icon={icon} />{" "}
              </span>
            ) : null}
            {image ? (
              <Image
                src={image}
                width={20}
                height={20}
                className="sidebar-icon svg-icon"
              />
            ) : null}

            <span className="sidebar-text">{title}</span>
          </span>
          {badgeText ? (
            <Badge
              pill
              bg={badgeBg}
              text={badgeColor}
              className="badge-md notification-count ms-2"
            >
              {badgeText}
            </Badge>
          ) : null}
        </Nav.Link>
      </Nav.Item>
    );
  };

  return (
    <>
      <Navbar
        expand={false}
        collapseOnSelect
        variant="dark"
        className="navbar-theme-primary px-4 d-md-none"
      >
        <Navbar.Brand
          className="me-lg-5"
          as={Link}
          to={Routes.DashboardOverview.path}
        >
          <Image src={ReactHero} className="navbar-brand-light" />
        </Navbar.Brand>
        <Navbar.Toggle
          as={Button}
          aria-controls="main-navbar"
          onClick={onCollapse}
        >
          <span className="navbar-toggler-icon" />
        </Navbar.Toggle>
      </Navbar>
      <CSSTransition timeout={300} in={show} classNames="sidebar-transition">
        <SimpleBar
          className={`collapse ${showClass} sidebar d-md-block bg-primary text-white`}
        >
          <div className="sidebar-inner px-4 pt-3">
            <div className="user-card d-flex d-md-none align-items-center justify-content-between justify-content-md-center pb-4">
              <div className="d-flex align-items-center">
                <div className="user-avatar lg-avatar me-4">
                  <Image
                    src={ProfilePicture}
                    className="card-img-top rounded-circle border-white"
                  />
                </div>
                <div className="d-block">
                  <h6>Hi, Jane</h6>
                  <Button
                    as={Link}
                    variant="secondary"
                    size="xs"
                    to={Routes.Presentation.path}
                    className="text-dark"
                  >
                    <FontAwesomeIcon icon={faSignOutAlt} className="me-2" />{" "}
                    Sign Out
                  </Button>
                </div>
              </div>
              <Nav.Link
                className="collapse-close d-md-none"
                onClick={onCollapse}
              >
                <FontAwesomeIcon icon={faTimes} />
              </Nav.Link>
            </div>
            <Nav className="flex-column pt-3 pt-md-0">
              {/* <NavItem title="Garanti Kongre" link={Routes.Presentation.path} image={ReactHero} /> */}
              <NavItem
                title="Gösterge Paneli"
                link={Routes.DashboardOverview.path}
                icon={faChartPie}
              />
              <NavItem
                title="Satışlar"
                link={Routes.SellTaable.path}
                icon={faMoneyCheckAlt}
              />
              <NavItem
                title="Tüm Katılımcılar"
                icon={faUsers}
                link={Routes.CompanyTable.path}
              />
              <NavItem
                title="Tüm Misafirler"
                link={Routes.GuestTable.path}
                icon={faUsers}
              />
              <NavItem
                title="Harcama Yapmayanlar"
                link={Routes.NoAttendTable.path}
                icon={faUsers}
              />

              {/* <NavItem title="Tüm Misafirler" link={Routes.NewCompany.path} icon={faUsers} /> */}

              <NavItem
                title="Konaklama"
                link={Routes.Acconmodation.path}
                icon={faBed}
              />

              {/* 
              <CollapsableNavItem eventKey="tables/" title="Tablolar" icon={faTable}>
                <NavItem title="Bootstrap Table" link={Routes.BootstrapTables.path} />
              </CollapsableNavItem> */}
              {/* <CollapsableNavItem eventKey="examples/" title="Page Examples" icon={faFileAlt}>
                <NavItem title="Sign In" link={Routes.Signin.path} />
                <NavItem title="Sign Up" link={Routes.Signup.path} />
                <NavItem title="Forgot password" link={Routes.ForgotPassword.path} />
                <NavItem title="Reset password" link={Routes.ResetPassword.path} />
                <NavItem title="Lock" link={Routes.Lock.path} />
                <NavItem title="404 Not Found" link={Routes.NotFound.path} />
                <NavItem title="500 Server Error" link={Routes.ServerError.path} />
              </CollapsableNavItem> */}
              <NavItem
                title="Konaklama Detay"
                link={Routes.DailyAccommodations.path}
                icon={faInfoCircle}
              />
              <div
                onClick={() =>
                  window.setTimeout(() => window.location.reload(),50)
                }
              >
                <NavItem
                  title="Proforma"
                  link={Routes.Proforma.path}
                  icon={faCreditCard}
                ></NavItem>
              </div>
              <div
                onClick={()=>window.setTimeout(() => window.location.reload(),50)}
              >
                <NavItem
                  title="Dış Katılım "
                  link={Routes.ExternalDetail.path}
                  icon={faExternalLinkSquareAlt}
                ></NavItem>
              </div>

              <NavItem></NavItem>
              <NavItem></NavItem>
              <NavItem></NavItem>
              <NavItem></NavItem>
              <NavItem></NavItem>
              <NavItem></NavItem>
              <Col className="mb-md-2">
                <Card.Link
                  href="#"
                  target="_blank"
                  className="d-flex justify-content-center"
                >
                  <Image
                    src={ThemesbergLogo}
                    height={35}
                    className="d-block mx-auto mb-3"
                    alt="Themesberg Logo"
                  />
                </Card.Link>
                <div
                  className="d-flex text-center justify-content-center align-items-center"
                  role="contentinfo"
                >
                  <p className="font-weight-small font-small mb-3">
                    Copyright © CSoft. All rights reserved
                  </p>
                </div>
              </Col>
              <Dropdown.Divider className="my-3 border-indigo" />
            </Nav>
          </div>
        </SimpleBar>
      </CSSTransition>
    </>
  );
};

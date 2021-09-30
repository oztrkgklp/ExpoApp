import React from "react";
import {
  faCoins,
  faUserCheck,
  faUserSlash,
  faUsers,
  faBed,
} from "@fortawesome/free-solid-svg-icons";
import { Col, Row } from "@themesberg/react-bootstrap";

import {
  CounterWidget,
  CircleChartWidget,
  ProgressTrackWidget,
  SalesValueWidget,
  SalesValueWidgetPhone,
} from "../../components/Widgets";
import { PageVisitsTable } from "../../components/Tables";
import { trafficShares } from "../../data/charts";
import { domain } from "../../assets/domain";
import useSWR from "swr";
import axios from "axios";
import {
  enteredCompany,
  notEnteredCompany,
  noPurchaseCount,
} from "../../components/FetchData";
var companiesEndorsement;
var companiesCount;
var accommodationsCount;

axios.get(domain + "companies/endorsement").then(function ({ data }) {
  // handle success
  companiesEndorsement = data.result;
  console.log(companiesEndorsement);
});
axios.get(domain + "companies/count").then(function ({ data }) {
  companiesCount = data.result;
  // handle success
});
axios.get(domain + "accommodations/count").then(function ({ data }) {
  accommodationsCount = data.result;
  // handle success
});

export default () => {
  const [entered, setEntered] = React.useState([]);
  const [notEntered, setNotEntered] = React.useState([]);
  const [noPurchaseount, setNoPurchaseount] = React.useState([]);

  React.useEffect(() => {
    const entered = async () => {
      const company = await enteredCompany();
      setEntered(company.result.length);
    };

    entered();
  }, []);
  React.useEffect(() => {
    const notEntered = async () => {
      const company = await notEnteredCompany();
      setNotEntered(company.result.length);
    };

    notEntered();
  }, []);
  React.useEffect(() => {
    const noPurchase = async () => {
      const company = await noPurchaseCount();
      setNoPurchaseount(company.result);
    };

    noPurchase();
  }, []);
  return (
    <>
      <Row className="justify-content-md-center">
        <Col xs={12} className="mb-4 d-none d-sm-block">
          <SalesValueWidget
            title="Satışlar"
            value="10,567"
            percentage={10.57}
          />
        </Col>
        <Col xs={12} className="mb-4 d-sm-none">
          <SalesValueWidgetPhone
            title="SatışLar"
            value="10,567"
            percentage={10.57}
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category={"Misafirler "}
            title={companiesCount == null ? "0" : companiesCount}
            percentage={18.2}
            icon={faUsers}
            iconColor="shape-secondary"
          />
        </Col>

        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Ciro"
            title={companiesEndorsement == null ? "0" :companiesEndorsement + "₺"}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCoins}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Konaklayan Misafir"
            title={accommodationsCount == null ? "0" :accommodationsCount}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faBed}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Katılan Firmalar"
            title={companiesCount == null ? "0" : companiesCount}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faUserCheck}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Katılmayan Firma"
            title={notEntered === null || "" ?notEntered ===   "0" : notEntered}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faUserSlash}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Harcama Yapmayanlar"
            title={noPurchaseount == null ? "0" :noPurchaseount }
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCoins}
            iconColor="shape-tertiary"
          />
        </Col>

        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CircleChartWidget title="Ciro Dağılımı" data={trafficShares} />
        </Col>
      </Row>
      <Row>
        <Col xs={12} xl={12} className="mb-4">
          <Row>
            <Col xs={12} xl={8} className="mb-4">
              <Row>
                <Col xs={12} className="mb-4">
                  <PageVisitsTable />
                </Col>
                <Col xs={12} lg={6} className="mb-4"></Col>
              </Row>
            </Col>
            <Col xs={12} xl={4}>
              <Row>
                <Col xs={12} className="mb-4">
                  <ProgressTrackWidget />
                </Col>
              </Row>
            </Col>
          </Row>
        </Col>
      </Row>
    </>
  );
};

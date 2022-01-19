import React from "react";
import {
  faCoins,
  faUserCheck,
  faUserSlash,
  faUsers,
  faBed,
  faBuilding
} from "@fortawesome/free-solid-svg-icons";
import { Col, Row } from "@themesberg/react-bootstrap";
import {
  CounterWidget,
  ProgressTrackWidget,
  } from "../../components/Widgets";
import { PageVisitsTable } from "../../components/Tables";
import { domain } from "../../assets/domain";
import axios from "axios";
import {
  enteredCompany,
  notEnteredCompany,
  noPurchaseCount,
  getGuests
} from "../../components/FetchData";
var companiesEndorsement;
var companiesCount;
var enteredCount;
var accommodationsCount;
var guestCount;
axios.get(domain + "companies/endorsement").then(function ({ data }) {
  // handle success
  companiesEndorsement = data.result;
  console.log(companiesEndorsement);
});
axios.get(domain + "companies/count").then(function ({ data }) {
  companiesCount = data.result;
  // handle success
});
axios.get(domain + "companies/entered/count").then(function ({ data }) {
  enteredCount = data.result;
  // handle success
});
axios.get(domain + "accommodations/count").then(function ({ data }) {
  accommodationsCount = data.result;
  // handle success
});
axios.get(domain + "guests/count").then(function ({ data }) {
  guestCount = data.result;
  // handle success
});
var formatter = new Intl.NumberFormat('tr-TR', {
  style: 'currency',
  currency: 'TRY',

  
});

export default () => {
  const [entered, setEntered] = React.useState([]);
  const [notEntered, setNotEntered] = React.useState([]);
  const [noPurchaseount, setNoPurchaseount] = React.useState([]);
  const [guests, setGuests] = React.useState(0.0);


  React.useEffect(() => {
    const entered = async () => {
      const company = await enteredCompany();
      setEntered(company.result.length);
    };

    entered();
  }, []);
  React.useEffect(() => {
    const guest = async () => {
      const company = await getGuests();
      var total = 0.0;
      company.result.map(c => {
        const {endorsement} = c
         total += parseFloat(endorsement)
      })
      setGuests(total);
    };

    guest();
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
      <Row style={{ marginTop: "50px" }} className="justify-content-md-center">
        {/* <Col xs={12} className="mb-4 d-none d-sm-block">
          <SalesValueWidget
            title="Satışlar"
            value={companiesEndorsement}
            percentage={10.57}
          />
        </Col>
        <Col xs={12} className="mb-4 d-sm-none">
          <SalesValueWidgetPhone
            title="SatışLar"
            value={companiesEndorsement}
            percentage={10.57}
          />
        </Col> */}
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category={"Firmalar "}
            title={companiesCount == null ? "0" : companiesCount}
            percentage={18.2}
            icon={faBuilding}
            iconColor="shape-secondary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category={"Misafirler"}
            title={guestCount == null ? "0" : guestCount}
            percentage={18.2}
            icon={faUsers}
            iconColor="shape-secondary"
          />
        </Col>

        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Ciro"
            title={
              formatter.format(companiesEndorsement)
            }
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCoins}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Konaklayan Misafir"
            title={accommodationsCount == null ? "0" : accommodationsCount}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faBed}
            iconColor="shape-tertiary"
          />
        </Col>

        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Katılmayan Firma"
            title={notEntered === null || "" ? notEntered === "0" : notEntered}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faUserSlash}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Toplam Satın Alma"
            title={formatter.format(guests)}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faCoins}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4"></Col>
        
        <Col xs={12} sm={6} xl={4} className="mb-4">
          <CounterWidget
            category="Katılan Firmalar"
            title={enteredCount == null ? "0" : enteredCount}
            period="5 Ekim - 8 Ekim"
            percentage={28.4}
            icon={faUserCheck}
            iconColor="shape-tertiary"
          />
        </Col>
        <Col xs={12} sm={6} xl={4} className="mb-4"></Col>
        {/* <Col xs={12} sm={6} xl={4} className="mb-4">
          <CircleChartWidget title="Katılımcı Dağılımı" data={trafficShares} />
        </Col> */}
      </Row>
      <Row>
        <Col xs={12} xl={12} className="mb-4">
          <Row style={{ marginTop: "70px" }}>
            <Col xs={12} xl={8} className="mb-4">
              {/* <Row>
                <Col xs={12} className="mb-4">
                  <PageVisitsTable style={{ height:"700px" }}/>
                </Col>
                <Col  xs={12} lg={6} className="mb-4"></Col>
              </Row> */}
            </Col>
            <Col xs={12} xl={11}>
              <Row style={{ marginTop: "3%",marginLeft:"10%" }}>
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

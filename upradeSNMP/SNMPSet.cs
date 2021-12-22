using SnmpSharpNet;
using System.Net;
using System;
using System.Collections.Generic;
using System.Windows;

namespace upradeSNMP
{
    class SNMPSet
    {
        Pdu _pdu;
        IpAddress _ip;
        UdpTarget _target;
        List<Oid> _oid;
        AgentParameters _aparam;

        public SNMPSet(string file, string ipTFTP, string codeReboot, List<string> oid, string community,int version)
        {
            _ip = new IpAddress();
            _target = new UdpTarget((IPAddress) new IpAddress());
            _pdu = new Pdu(PduType.Set);
            _oid = new List<Oid>();
            try
            {
                foreach (string str in oid)
            {
                _oid.Add(new Oid(str));
            }
            
                if (file != "")
                    _pdu.VbList.Add(_oid[0], new OctetString(file));//Стрка для имени файла прошивки

                _pdu.VbList.Add(_oid[1], new IpAddress(ipTFTP));//строка для адреса сервера

                _pdu.VbList.Add(_oid[2], new Integer32(Convert.ToInt32(codeReboot)));//команда на перезагрузку
            }
            catch(Exception ex)
            {
                MessageBox.Show("Введите OIDы для TFTP сервера и команды на перезагрузку\n"+ ex.Message);
                
            }


            switch (version)
            {
                case 1:
                    _aparam = new AgentParameters(SnmpVersion.Ver1,new OctetString(community));
                    break;
                case 2:

                    _aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString(community));
                    break;

                case 3:
                    _aparam = new AgentParameters(SnmpVersion.Ver3, new OctetString(community));
                    break;

                default:
                    _aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString(community));
                    break;
            
            }



        }
        public void start(string ipAddress)
        {
            _ip.Set(ipAddress);
            _target.Address = (IPAddress)_ip;
            try
            {
                switch (_aparam.GetVersion())
                {
                    case 0:
                        {
                            SnmpV1Packet response = _target.Request(_pdu, _aparam) as SnmpV1Packet;
                            break;
                        }
                    case 1:
                        {
                            SnmpV2Packet response = _target.Request(_pdu, _aparam) as SnmpV2Packet;
                            break;
                        }
                    case 2:
                        {
                            SnmpV3Packet response = _target.Request(_pdu, _aparam) as SnmpV3Packet;
                            break;
                        }
                    default:

                        break;
                }
            }
             catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка:\n ", ex.Message+"\n Процесс прерван");
                _target.Close();
            }
            _target.Close();
          
           

        }
    }



}


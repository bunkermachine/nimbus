<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CloudWS" generation="1" functional="0" release="0" Id="a0db2638-12d3-41fa-afcf-e35516c35b6f" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CloudWSGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="SandwichServices:HttpIn" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CloudWS/CloudWSGroup/LB:SandwichServices:HttpIn" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="SandwichServicesInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CloudWS/CloudWSGroup/MapSandwichServicesInstances" />
          </maps>
        </aCS>
        <aCS name="SandwichServices:DiagnosticsConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CloudWS/CloudWSGroup/MapSandwichServices:DiagnosticsConnectionString" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:SandwichServices:HttpIn">
          <toPorts>
            <inPortMoniker name="/CloudWS/CloudWSGroup/SandwichServices/HttpIn" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapSandwichServicesInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CloudWS/CloudWSGroup/SandwichServicesInstances" />
          </setting>
        </map>
        <map name="MapSandwichServices:DiagnosticsConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CloudWS/CloudWSGroup/SandwichServices/DiagnosticsConnectionString" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="SandwichServices" generation="1" functional="0" release="0" software="C:\My Stuff\My Work\Stanford\CS210\CloudWS\CloudWS\obj\Debug\SandwichServices\" entryPoint="base\x86\WaWebHost.exe" parameters="" memIndex="1792" hostingEnvironment="frontendfulltrust" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="HttpIn" protocol="http" />
            </componentports>
            <settings>
              <aCS name="DiagnosticsConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;SandwichServices&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;SandwichServices&quot;&gt;&lt;e name=&quot;HttpIn&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CloudWS/CloudWSGroup/SandwichServicesInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="SandwichServicesInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="61333b7a-a37d-47e3-abb3-747aacbaac95" ref="Microsoft.RedDog.Contract\ServiceContract\CloudWSContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="d78d49e7-d1d7-41e3-91e4-d006d7bce770" ref="Microsoft.RedDog.Contract\Interface\SandwichServices:HttpIn@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CloudWS/CloudWSGroup/SandwichServices:HttpIn" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>
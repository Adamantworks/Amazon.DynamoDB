using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Adamantworks.Amazon.DynamoDB")]
[assembly: AssemblyDescription("An alternate SDK for Amazon DynamoDB with a cleaner API and advanced operations. Support async and sync.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Adamantworks")]
[assembly: AssemblyProduct("Admantworks DynamoDB API")]
[assembly: AssemblyCopyright("Copyright © 2015 Adamantworks.  All Rights Reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4513c642-c4d3-4855-8786-bebe74eddf03")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.5.1.0")]

[assembly: CLSCompliant(true)]
#if DEBUG
[assembly: InternalsVisibleTo("Adamantworks.Amazon.DynamoDB.Tests")]
#endif
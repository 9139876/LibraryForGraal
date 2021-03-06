﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Graal.Library.Storage.Common.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Graal.Library.Storage.Common.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to drop schema if exists new_graal cascade;
        ///
        ///create schema new_graal;
        ///
        ///
        ///create table new_graal.quotes_parser_expressions(
        ///    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY
        ///    ,specification text NULL
        ///    ,CONSTRAINT quotes_parser_expressions_pkey PRIMARY KEY (id)
        ///);
        ///
        ///insert into new_graal.quotes_parser_expressions(specification) values (null);
        ///
        ///create table new_graal.tickersinfoes(
        ///    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY    
        ///    ,ticker_title text NOT NULL    
        ///    ,market_title text  [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string sql {
            get {
                return ResourceManager.GetString("sql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to drop schema if exists %schema_name% cascade|
        ///
        ///create schema %schema_name%|
        ///
        ///create table %schema_name%.quotes_parser_expressions(
        ///    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY
        ///    ,specification text NULL
        ///    ,CONSTRAINT quotes_parser_expressions_pkey PRIMARY KEY (id)
        ///)|
        ///
        ///insert into %schema_name%.quotes_parser_expressions(specification) values (null)|
        ///
        ///create table %schema_name%.tickersinfoes(
        ///    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY
        ///    ,ticker_title text NOT NULL
        ///    ,market_t [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string sql_create_commands {
            get {
                return ResourceManager.GetString("sql_create_commands", resourceCulture);
            }
        }
    }
}
